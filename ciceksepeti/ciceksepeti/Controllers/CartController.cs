using ciceksepeti.Models;
using ciceksepeti.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using ciceksepeti.Entities;
using System.Security.Claims;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ciceksepeti.Migrations;

namespace ciceksepeti.Controllers
{
    public class CartController : Controller
    {
        private readonly ISession _session;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CartController> _logger;



        public CartController(IHttpContextAccessor httpContextAccessor, AppDbContext appDbContext, ILogger<CartController> logger)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _appDbContext = appDbContext;
            _logger = logger;
        }

        // Sepete ürün ekleme action'ı
        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal price, string imageUrl)
        {
            var cart = _session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            var existingItem = cart.FirstOrDefault(item => item.ProductId == productId);

            if (existingItem != null)
            {
                // Eğer zaten varsa, sadece miktarını arttır
                existingItem.Quantity++;
            }
            else
            {
                // Yeni ürün sepete ekleniyor
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    Price = price,
                    ImageUrl = imageUrl,
                    Quantity = 1
                });
                TempData["CartMessage"] = "Ürün sepete eklendi!";

            }

            // Güncellenmiş sepeti session'a kaydediyoruz
            _session.SetObject("Cart", cart);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        // Sepet sayfasını görüntüleme
        public IActionResult Index()
        {
            var cart = _session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Toplam fiyat hesaplama
            decimal totalAmount = cart.Sum(CartItem => CartItem.Price * CartItem.Quantity);  // Sepetteki tüm ürünlerin toplam fiyatını hesapla

            ViewBag.TotalAmount = totalAmount;

            return View(cart);
        }
        [HttpPost]
        public IActionResult ConfirmCart()
        {
            var cart = _session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            if (cart.Count == 0)
            {
                TempData["Message"] = "Sepetinizde ürün bulunmamaktadır.";
                return RedirectToAction("Index", "Home");
            }

            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "Lütfen giriş yapın.";
                return RedirectToAction("Login", "Account");
            }

            // Kullanıcıyı session'dan alıyoruz
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Message"] = "Geçersiz kullanıcı ID.";
                return RedirectToAction("Login", "Account");
            }

            // Yeni bir sipariş (Order) oluşturuyoruz
            var order = new Order
            {
                OrderDate = DateTime.Now,
                TotalAmount = cart.Sum(item => item.Price * item.Quantity),
                OrderStatus = "Done",  // Sipariş durumu
                UserId = int.Parse(userId),
                OrderItems = cart.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    UserId = int.Parse(userId),
                    ProductName = item.ProductName,
                    Price = item.Price,
                    TotalPrice = item.Price * item.Quantity,
                    Quantity = item.Quantity
                }).ToList(),
            };

            // Siparişi veritabanına ekliyoruz
            try
            {
                _appDbContext.Orders.Add(order);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya mesaj veriyoruz
                _logger.LogError($"Hata oluştu: {ex.Message}");
                _logger.LogError($"Stack Trace: {ex.StackTrace}");
                return RedirectToAction("Index", "Home");
            }

            // Sipariş ID'sini session'a kaydediyoruz
            _session.SetInt32("OrderId", order.OrderId);

            // Kullanıcıyı ödeme sayfasına yönlendiriyoruz
            return RedirectToAction("Payment");
        }

        public IActionResult Payment()
        {
            return View();
        }

        // Kişisel bilgileri alma ve kaydetme
        [HttpPost]
        public IActionResult SavePersonalInfo(PaymentViewModel model)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "Lütfen giriş yapın.";
                return RedirectToAction("Login", "Account");
            }

            // Kullanıcıyı session'dan alıyoruz
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Message"] = "Geçersiz kullanıcı ID.";
                return RedirectToAction("Login", "Account");
            }

            var orderId = HttpContext.Session.GetInt32("OrderId");
            if (!orderId.HasValue)
            {
                TempData["Message"] = "Geçersiz sipariş ID.";
                return RedirectToAction("Index", "Home");
            }

            // Kullanıcı bilgilerini veritabanına kaydediyoruz
            var orderInformation = new OrderInformation
            {
                UserName = model.UserName,
                UserPhone = model.UserPhone,
                City = model.City,
                District = model.District,
                Neighborhood = model.Neighborhood,
                Address = model.Address,
                UserId = int.Parse(userId),
                OrderId = orderId.Value,
            };

            _appDbContext.OrderInformation.Add(orderInformation);
            _appDbContext.SaveChanges();

            return RedirectToAction("CardInformation", "Cart");
        }

        [HttpGet]

        public IActionResult CardInformation()
        {
            // Sepeti session'dan alıyoruz
            var cart = _session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Toplam fiyat hesaplama
            decimal totalAmount = cart.Sum(CartItem => CartItem.Price * CartItem.Quantity);  // Sepetteki tüm ürünlerin toplam fiyatını hesapla
            int totalItems = cart.Sum(CartItem => CartItem.Quantity);  // Sepetteki toplam ürün sayısını hesapla

            // Verileri ViewBag ile view'a gönder
            ViewBag.TotalAmount = totalAmount;
            ViewBag.TotalItems = totalItems;

            return View();
        }





        [HttpPost]
        public IActionResult SaveCardInformation(CardViewModel model)

        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "Lütfen giriş yapın.";
                return RedirectToAction("Login", "Account");
            }

            // Kullanıcıyı session'dan alıyoruz
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Message"] = "Geçersiz kullanıcı ID.";
                return RedirectToAction("Login", "Account");
            }

            var orderId = HttpContext.Session.GetInt32("OrderId");
            if (!orderId.HasValue)
            {
                TempData["Message"] = "Geçersiz sipariş ID.";
                return RedirectToAction("Index", "Home");
            }
            var cardInformation = new CardInformation
            {

                CardNumber = model.CardNumber,
                Month = model.Month,
                Year = model.Year,
                CVV = model.CVV,
                UserId = int.Parse(userId),
                OrderId = orderId.Value,
            };

            try
            {
                _appDbContext.CardInformation.Add(cardInformation);
                _appDbContext.SaveChanges();
                return RedirectToAction("Success", "Cart");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Kart bilgileri kaydedilemedi: " + ex.Message;
                return RedirectToAction("CardInformation", "Cart");
            }
        }
        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = _session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Sepetteki ürünü bul
            var itemToRemove = cart.FirstOrDefault(item => item.ProductId == productId);

            if (itemToRemove != null)
            {
                // Ürünü listeden kaldır
                cart.Remove(itemToRemove);
            }

            // Güncellenmiş sepeti session'a kaydediyoruz
            _session.SetObject("Cart", cart);

            // Sepet sayfasına geri yönlendir
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public IActionResult AddToFavorites(int productId, string productName, decimal price, string imageUrl)
        {
            var favorites = _session.GetObject<List<Product>>("Favorites") ?? new List<Product>();
            var existingItem = favorites.FirstOrDefault(item => item.ProductID == productId);

            if (existingItem == null)
            {
                // Favorilere ekleme
                favorites.Add(new Product
                {
                    ProductID = productId,
                    Name = productName,
                    Price = price,
                    ImageUrl = imageUrl
                });

                TempData["FavoriteMessage"] = "Ürün favorilere eklendi!";

            }

            // Favorileri session'a kaydet
            _session.SetObject("Favorites", favorites);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        // Favorilerden çıkarma işlemi
        [HttpPost]
        public IActionResult RemoveFromFavorites(int productId)
        {
            var favorites = _session.GetObject<List<Product>>("Favorites") ?? new List<Product>();
            var existingItem = favorites.FirstOrDefault(item => item.ProductID == productId);

            if (existingItem != null)
            {
                // Favorilerden çıkarma
                favorites.Remove(existingItem);
            }

            // Favorileri session'a kaydet
            _session.SetObject("Favorites", favorites);

            return RedirectToAction("Favorites", "Cart"); // Favoriler sayfasına geri dön
        }

        // Favoriler sayfasını görüntüleme
        public IActionResult Favorites()
        {
            var favorites = _session.GetObject<List<Product>>("Favorites") ?? new List<Product>();
            return View(favorites); // Favoriler sayfasına ürünleri gönder
        }

        // Favorilere eklenmiş ürünün favorilerde olup olmadığını kontrol etme
        public IActionResult IsProductInFavorites(int productId)
        {
            
            var favorites = _session.GetObject<List<Product>>("Favorites") ?? new List<Product>();

            var products = _appDbContext.Yields.ToList();

            ViewBag.Favorites = favorites.Select(f => f.ProductID).ToList();

            return View(products);


        } 
    }

}

    



   



       
