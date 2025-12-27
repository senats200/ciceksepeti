using ciceksepeti.Entities;
using ciceksepeti.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ciceksepeti.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return View("SearchResults", new List<Product>()); // Boş sonuç döner
            }

            var results = _context.Yields
                .Where(p => p.Name.Contains(query) || p.Category.Contains(query))
                .ToList();

            return View("SearchResults", results);
        }
    }
}
