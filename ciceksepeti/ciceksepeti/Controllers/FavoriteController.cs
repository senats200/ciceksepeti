using Microsoft.AspNetCore.Mvc;

namespace ciceksepeti.Controllers
{
    public class FavoriteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
