using ciceksepeti.Entities;
using ciceksepeti.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ciceksepeti.Services
{
    public class SalesService
    {
        private readonly AppDbContext _context;

        public SalesService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<AllSales>> GetAllOrdersAsync()
        {
            var sales = await _context.Set<AllSales>()
                .FromSqlRaw("EXEC GetAllOrders")  
                .ToListAsync();  

            return sales;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            // SP'yi çalıştırıyoruz ve sonucu User türünde alıyoruz
            return await _context.Users
                .FromSqlRaw("EXEC GetUsers")
                .ToListAsync();
        }
        public async Task<decimal> GetTotalProfitAsync()
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "EXEC CalculateTotalProfitProcedure";
                var result = await command.ExecuteScalarAsync();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }

        public async Task<List<WeeklySales>> GetWeeklySalesReportAsync()
        {
            var currentDayOfWeek = (int)DateTime.Now.DayOfWeek;

            // Eğer gün Pazar ise, haftanın başlangıcı 1 gün önceki Pazartesi
            var weekStartDate = DateTime.Now.AddDays(currentDayOfWeek == 0 ? -6 : -currentDayOfWeek + 1);
            var weekEndDate = weekStartDate.AddDays(6);

            var weekStartDateParam = new SqlParameter("@WeekStartDate", SqlDbType.DateTime2) { Value = weekStartDate.Date };
            var weekEndDateParam = new SqlParameter("@WeekEndDate", SqlDbType.DateTime2) { Value = weekEndDate.Date };

            // Stored Procedure'ü çağırıyoruz ve sonucu WeeklySalesReport listesine dönüştürüyoruz
            var weeklySales = await _context.WeeklySales.FromSqlRaw(
                "EXEC GetWeeklySalesReport ",
                weekStartDateParam,
                weekEndDateParam
            ).ToListAsync();

            return weeklySales;
        }




    }
}
