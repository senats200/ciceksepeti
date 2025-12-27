using ciceksepeti.Entities;
using ciceksepeti.Models;
using ciceksepeti.Models.ciceksepeti.Models;
using Microsoft.EntityFrameworkCore;

namespace ciceksepeti.Services
{
    public class CreditCardService
    {
        private readonly AppDbContext _context;

        public CreditCardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CreditCardInfo>> GetCreditCardInfoAsync(int userId)
        {
        
            var creditCardInfoList = await _context.Set<CreditCardInfo>()
             .FromSqlRaw("EXEC GetCreditCardInfo @UserId = {0}", userId)
             .ToListAsync();

            Console.WriteLine($"Number of Credit Cards: {creditCardInfoList.Count}");
            foreach (var card in creditCardInfoList)
            {
                Console.WriteLine($"Card Number: {card.CardNumber}, Month: {card.Month}, Year: {card.Year}");
            }

            return creditCardInfoList;

        }
    }
}
