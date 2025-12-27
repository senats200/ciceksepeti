using ciceksepeti.Entities;

namespace ciceksepeti.Models
{
    public class CardInformation
    {
        public int CardInformationId { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string CardNumber { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int CVV { get; set; }
        public UserAccount User { get; set; } 


    }
}
