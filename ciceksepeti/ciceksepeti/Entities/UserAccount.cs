using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ciceksepeti.Entities
{
    [Index(nameof(Email), IsUnique=true )]
    [Index(nameof(UserName), IsUnique = true)]

    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Ad Soyad girmek gereklidir.")]
        [MaxLength(50,ErrorMessage = "Maksimum 50 karakter girebilirsiniz.")]
        public string UserName { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Email adresi girmek gereklidir.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Şifre girmek gereklidir.")]
        [MaxLength(20, ErrorMessage = "Maksimum 20 karakter girebilirsiniz.")]
        public string Password { get; set; }

        public string Role { get; set; }


    }
}
