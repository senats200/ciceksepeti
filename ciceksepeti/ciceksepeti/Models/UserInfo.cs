using System.ComponentModel.DataAnnotations;

namespace ciceksepeti.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string Phone { get; set; }
        [Required]

        public string Address { get; set; }
        public DateTime UpdateDate { get; set; }
    }

}
