namespace ciceksepeti.Models
{
    public class UpdatedProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
