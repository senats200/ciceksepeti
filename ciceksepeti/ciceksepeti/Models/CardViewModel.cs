using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ciceksepeti.Models
{
    public class CardViewModel
    {

        [Required]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Kart numarası 16 haneli olmalıdır.")]
        public string CardNumber { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Geçerli bir ay giriniz.")]
        public int Month { get; set; }

        [Required]
        [Range(2023, 2035, ErrorMessage = "Geçerli bir yıl giriniz.")]
        public int Year { get; set; }

        [Required]
        public int CVV { get; set; }

        [Required(ErrorMessage = "Şartları kabul etmelisiniz.")]
        public bool TermsAccepted { get; set; }
    }
}
