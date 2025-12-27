using ciceksepeti.Entities;
using ciceksepeti.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Data;
using System.Security.Claims;

namespace ciceksepeti.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserAccount account = new UserAccount();
                account.Email = model.Email;
                account.Password = model.Password;
                account.UserName = model.UserName;
                account.Role = string.IsNullOrEmpty(model.Role) ? "User" : model.Role;


                try
                {
                    _context.Members.Add(account);
                    _context.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Login", "Account");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                    return View(model);
                }
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Console.WriteLine("Validasyon Hataları: " + string.Join(", ", errors));
            }

            return View(model);
        }
        public IActionResult Login() 
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model, bool isAdmin = false)
        {
            {
                if (ModelState.IsValid)
                {
                    // Admin girişi için kontrol
                    if (isAdmin)
                    {
                        // Admin kullanıcı kontrolü (Admin kullanıcılarının Role'u "Admin" olmalı)
                        var admin = _context.Members.Where(x => x.Email == model.Email && x.Password == model.Password && x.Role == "Admin").FirstOrDefault();

                        if (admin != null )
                        {
                            // Admin başarılı giriş yaptıysa, kullanıcıyı admin paneline yönlendir
                            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, admin.Email),
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                            HttpContext.Session.SetString("UserId", admin.UserID.ToString());
                            HttpContext.Session.SetString("UserEmail", admin.Email);

                            return RedirectToAction("Index", "Home"); // Admin'e özel sayfaya yönlendir
                        }
                        else
                        {
                            // Eğer admin bulunamazsa hata mesajı göster
                            ModelState.AddModelError("", "Admin girişi başarısız. Lütfen doğru admin bilgilerini girin.");
                        }
                    }
                    else
                    {
                        // Normal kullanıcı girişi
                        var user = _context.Members.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
                        if (user != null)
                        {
                            

                                // Normal kullanıcı başarılı giriş yaptıysa
                                var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Email),
                                new Claim(ClaimTypes.Role, "User"),
                            };

                                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                                HttpContext.Session.SetString("UserId", user.UserID.ToString());
                                HttpContext.Session.SetString("UserEmail", user.Email);

                                return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendir
                            }
                        

                        else
                        {
                            ModelState.AddModelError("", "Email ya da şifre hatalı.");
                        }
                    }
                }

                return View();
            }
        }
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            // Şifrelerin eşleştiğini kontrol et
            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Şifreler eşleşmiyor.");
                return View(model);
            }

            // E-posta adresiyle kullanıcıyı bul
            var user = _context.Members.FirstOrDefault(x => x.Email ==model. Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(model);
            }

            // Yeni şifreyi düz metin olarak güncelle
            user.Password =model. NewPassword;

            // Değişiklikleri kaydet
            _context.SaveChanges();

            // Giriş sayfasına yönlendir
            return RedirectToAction("Login", "Account");
        }
        

        public IActionResult Logout()
            {
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
            }


    }


}


