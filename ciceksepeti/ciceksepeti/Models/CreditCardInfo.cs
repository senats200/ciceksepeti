using Microsoft.EntityFrameworkCore;

namespace ciceksepeti.Models
{
    [Keyless]
    public class CreditCardInfo
    {
        public int UserId { get; set; }
        public string CardNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }   
        public int CVV { get; set; }
    }
}
