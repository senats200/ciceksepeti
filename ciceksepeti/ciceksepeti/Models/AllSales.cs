using Microsoft.EntityFrameworkCore;

namespace ciceksepeti.Models
{
    [Keyless]
    public class AllSales
        {
            public int OrderId { get; set; }
            public string UserName { get; set; }  // Kullanıcı adı
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
            public DateTime OrderDate { get; set; }
            public string OrderStatus { get; set; }
        }

}

