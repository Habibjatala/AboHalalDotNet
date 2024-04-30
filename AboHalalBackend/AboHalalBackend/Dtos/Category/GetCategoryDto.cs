
using AboHalalBackend.Dtos.Product;

namespace AboHalalBackend.Dtos.Category
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
       public ICollection<ProductDto> Products { get; set; }
    }
}
