using ciceksepeti.Entities;
using ciceksepeti.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ciceksepeti.Controllers
{
    public class SalesController : Controller
    {
        private readonly SalesService _salesService;
        private readonly AppDbContext _context;
        public SalesController(SalesService salesService,AppDbContext context)
        {
            _salesService = salesService;
            _context=context;
        }

        public async Task<IActionResult> SalesReport()
        {
            // SalesService'den satış verilerini alıyoruz
            var sales = await _salesService.GetAllOrdersAsync();
            return View("SalesReport",sales);
        }
        public async Task<IActionResult> GetUsers()
        {
            var users = await _salesService.GetUsersAsync();  // Kullanıcıları alıyoruz
            return View("GetUsers",users);  
        }
    

        public async Task<IActionResult> WeeklySalesReport()
        {
        

            var weeklySalesReport = await _salesService.GetWeeklySalesReportAsync();
            var totalProfit = await _salesService.GetTotalProfitAsync();

            string profitClass;
            if (totalProfit > 0)
                profitClass = "text-success"; // Yeşil
            else if (totalProfit < 0)
                profitClass = "text-danger"; // Kırmızı
            else
                profitClass = "text-primary";

            ViewBag.TotalProfit = totalProfit;
            ViewBag.ProfitClass = profitClass;
            return View("WeeklySalesReport",weeklySalesReport);
            

        }



    }
}
