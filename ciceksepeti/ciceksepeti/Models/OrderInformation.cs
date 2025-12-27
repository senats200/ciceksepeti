using ciceksepeti.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ciceksepeti.Models
{
    public class OrderInformation
    {
        public  int  OrderInformationId { get; set; }
        public int UserId { get; set; } 
        public int OrderId {  get; set; }
        public  string UserName { get; set; }
        public  string UserPhone { get; set; }
        public  string City { get; set; }
        public  string District { get; set; }
        public  string  Neighborhood { get; set; }
        public  string Address { get; set; }
        // İlgili Sipariş ID'si
        public UserAccount User { get; set; } // İlişkili Sipariş
    }
}
