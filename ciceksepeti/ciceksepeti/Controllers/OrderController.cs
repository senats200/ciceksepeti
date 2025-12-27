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
using ciceksepeti.Services;


namespace ciceksepeti.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }


        public async Task<IActionResult> UserOrders()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var orders = await _orderService.GetOrders(int.Parse(userId));

            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);  // Siparişleri View'a gönder
        }
      
    }
}
