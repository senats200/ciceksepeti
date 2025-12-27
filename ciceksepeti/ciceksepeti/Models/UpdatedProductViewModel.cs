namespace ciceksepeti.Models
{
    public class UpdatedProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public List<Product> Yields { get; set; } // Tüm ürünler
        public List<UpdatedProduct> UpdatedProducts { get; set; } // Güncellenen ürünler
    }
}
