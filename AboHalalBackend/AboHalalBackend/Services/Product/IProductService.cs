using AboHalalBackend.Dtos.Product;
using AboHalalBackend.Models;

namespace AboHalalBackend.Services.Product
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductDto>> AddProductToCategory(ProductDto productDto);
    }
}
