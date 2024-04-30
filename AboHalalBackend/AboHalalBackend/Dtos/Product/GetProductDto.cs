namespace AboHalalBackend.Dtos.Product
{
    public class GetProductDto
    {
        public string? title { get; set; }
        public long? discount { get; set; }
        public long? price { get; set; }
        public string? description { get; set; }

        public int? CategoryId { get; set; }
        public AboHalalBackend.Models.Category? Category { get; set; }
        public IFormFile? ImageFile { get; set; }

        public IFormFile? CertificateFile { get; set; }
    }
}
