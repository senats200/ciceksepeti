using ciceksepeti.Views.Shared;

namespace ciceksepeti.Models
{
    public class CartModel
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal TotalAmount => Items.Sum(item => item.TotalPrice);  // Sepetin toplam fiyatı
    }

}
