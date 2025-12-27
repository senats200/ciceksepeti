using Microsoft.EntityFrameworkCore;

namespace ciceksepeti.Models
{
    namespace ciceksepeti.Models
    {
        [Keyless]
        public class OrderDetail
        {

            public int OrderID { get; set; }
            public int UserId { get; set; }
            public DateTime OrderDate { get; set; }
            public string OrderStatus { get; set; }

            public int ProductID { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal TotalPrice { get; set; }
            public Product Product { get; set; }

        }
    }

}
