namespace AboHalalBackend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string title { get; set; }
        public long discount { get; set; }
        public long price { get; set; }
        public string description { get; set; }
        public string Picture { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
