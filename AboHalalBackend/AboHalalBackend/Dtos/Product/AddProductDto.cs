using System.ComponentModel.DataAnnotations.Schema;
using AboHalalBackend.Models;

namespace AboHalalBackend.Dtos.Product
{
    public class AddProductDto
    {
        public string? title { get; set; }
        public long? discount { get; set; }
        public long? price { get; set; }
        public string? description { get; set; }
        public int? CategoryId { get; set; }
      
        public string? Picture { get; set; }
        public IFormFile? ImageFile { get; set; }



    }
}
