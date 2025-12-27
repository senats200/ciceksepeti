
using ciceksepeti.Data;
using ciceksepeti.Entities;
using ciceksepeti.Models.ciceksepeti.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ciceksepeti.Controllers;
using ciceksepeti.Models;


namespace ciceksepeti.Services
{
  public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

      public async Task<List<OrderDetail>> GetOrders(int userId)
        {
            var orders = await _context.Set<OrderDetail>()
            .FromSqlRaw("EXEC GetOrders @UserId = {0}", userId)
            .ToListAsync();

            return orders;
        }

    }

}
