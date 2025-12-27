using ciceksepeti.Entities;
using ciceksepeti.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net;

namespace ciceksepeti.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> UserInfo()
        {
            // Kullanıcı ID'sini session'dan alıyoruz
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = int.Parse(userIdString); // Güvenli şekilde int'e dönüştürülür

            // Kullanıcı bilgilerini veritabanından alıyoruz
            var userInfo = await _context.UserInfo
                                   .Where(ui => ui.UserId == userId)
                                   .OrderByDescending(ui => ui.UpdateDate)  // En son güncellenen veriyi al
                                   .FirstOrDefaultAsync();

            return View(userInfo);  // En son güncellenen veriyi formda göstereceğiz
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserInfo(UserInfo model)
        {
            if (ModelState.IsValid)
            {
                var userIdString = HttpContext.Session.GetString("UserId");

                if (string.IsNullOrEmpty(userIdString))
                {
                    return RedirectToAction("Login", "Account");
                }

                var userId = int.Parse(userIdString);
                var userInfo = await _context.UserInfo.FirstOrDefaultAsync(ui => ui.UserId == userId);

                if (userInfo != null)
                {
                    // Veriyi güncelle
                    userInfo.Name = model.Name;
                    userInfo.Email = model.Email;
                    userInfo.Phone = model.Phone;
                    userInfo.Address = model.Address;
                    userInfo.UpdateDate = DateTime.Now; // Güncellenen tarih

                    // Veriyi kaydet
                    _context.UserInfo.Update(userInfo);
                }
                else
                {
                    // Kullanıcı bulunamadıysa yeni bir kayıt oluştur
                    userInfo = new UserInfo
                    {
                        UserId = userId,
                        Name = model.Name,
                        Email = model.Email,
                        Phone = model.Phone,
                        Address = model.Address,
                        UpdateDate = DateTime.Now
                    };
                    _context.UserInfo.Add(userInfo);
                }
                var member = await _context.Members.FirstOrDefaultAsync(m => m.UserID == userId);
                if (member != null)
                {
                    member.Email = model.Email;  // Email güncelle
                    _context.Members.Update(member);  // Veriyi güncelle
                }

                var orderInformation = await _context.OrderInformation.FirstOrDefaultAsync(m => m.UserId == userId);
                if (orderInformation != null)
                {
                    orderInformation.UserPhone = model.Phone;  // Telefon numarasını güncelle (opsiyonel)
                    _context.OrderInformation.Update(orderInformation);  // Veriyi güncelle
                }
                await _context.SaveChangesAsync();  // Veriyi kaydet

                // Başarı mesajı
                TempData["SuccessMessage"] = "Bilgileriniz başarıyla kaydedilmiştir.";
                return RedirectToAction("UserInfo");  // Formu tekrar göster
            }

            return View(model);  // Eğer hata varsa, formu tekrar göster
        }
    }





}


    
    

