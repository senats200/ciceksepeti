using System.ComponentModel.DataAnnotations;

namespace ciceksepeti.Models
{
    public class RegisterViewModel
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Ad Soyad girmek gereklidir.")]
        [MaxLength(50, ErrorMessage = "Maksimum 50 karakter girebilirsiniz.")]
        public string UserName { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Email adresi girmek gereklidir.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir email adresi giriniz.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Lütfen geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Şifre girmek gereklidir.")]
        [StringLength(20, MinimumLength =6 ,ErrorMessage = "Maksimum 20 yada minimum 6 karakter girmelisiniz.")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Lütfen şifrenizi doğrulayınız.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public  required string Role { get; set; }


    }
}
