namespace ciceksepeti.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }  // Sipariş ile ilişkilendirmek için
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        // Siparişle ilişkilendirme
        public  Order Order { get; set; }
    }

}
