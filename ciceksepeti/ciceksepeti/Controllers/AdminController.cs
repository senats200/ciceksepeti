using ciceksepeti.Entities;
using ciceksepeti.Models;
using ciceksepeti.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Data;
using System.Security.Claims;
namespace ciceksepeti.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        public IActionResult AdminLogin()
        {
            return View();
        }

     
    }
}
