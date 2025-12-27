using ciceksepeti.Services;
using Microsoft.AspNetCore.Mvc;

namespace ciceksepeti.Controllers
{

    public class CreditCardController : Controller
    {
        private readonly CreditCardService _service;

        public CreditCardController(CreditCardService service)
        {
            _service = service;
        }

        public async Task<IActionResult> CreditCardInfo()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (userIdString == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var userId = int.Parse(userIdString);
            var creditCardInfos = await _service.GetCreditCardInfoAsync(userId);
            if (creditCardInfos == null)
            {
                return NotFound();
            }
            Console.WriteLine($"Number of Credit Cards: {creditCardInfos.Count}");
            foreach (var card in creditCardInfos)
            {
                Console.WriteLine($"Card Number: {card.CardNumber}, Month: {card.Month}, Year: {card.Year}");
            }

            return View(creditCardInfos);


        }
    }

    
}
