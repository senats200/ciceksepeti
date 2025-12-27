namespace ciceksepeti.Models
{
    public class Order
    {
        public  int OrderId { get; set; }
        public  int UserId { get; set; }  // Kullanıcı ile ilişkilendirmek için
        public  DateTime OrderDate { get; set; }
        public  decimal TotalAmount { get; set; }
        public  string OrderStatus { get; set; } // Örneğin: "Yeni", "Onaylandı", "Gönderildi", vb.


        // Siparişe ait ürünler
        public  List<OrderItem> OrderItems { get; set; }
    }

}
