using ciceksepeti.Data;
using ciceksepeti.Entities;
using ciceksepeti.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ciceksepeti.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepository _repository;
        private readonly AppDbContext _context;

        // Constructor, repository'i baþlatýyoruz
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _repository = new ProductRepository(context); // Repository'i baþlatýyoruz
            _context = context;
        }

   
        public IActionResult Index()
        {
             return View();
        }
    

        // Privacy sayfasý
        public IActionResult Privacy()
        {
            return View();
        }
       

        // Error sayfasý
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Kokina kategorisi için aksiyon
        public IActionResult Kokina()
        {
            var products = _context.Yields.ToList();
            return View(products); // Kokina.cshtml View'ini döndürüyoruz
        }

        // Saksý Çiçekleri kategorisi için aksiyon
        public IActionResult Saksicicekleri()
        {
            var products = _context.Yields.ToList();
            return View(products); // Saksicicekleri.cshtml View'ini döndürüyoruz
        }

        // Doðum Günü Çiçekleri kategorisi için aksiyon
        public IActionResult DogumGunuCicekleri()
        {
            var products = _context.Yields.ToList();
            return View(products); // DogumGunuCicekleri.cshtml View'ini döndürüyoruz
        }

        // Yýldönümü Çiçekleri kategorisi için aksiyon
        public IActionResult YildonumuCicekleri()
        {
            var products = _context.Yields.ToList();
            return View(products); // YildonumuCicekleri.cshtml View'ini döndürüyoruz
        }

       
    }
}
