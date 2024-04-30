using AboHalalBackend.Dtos.Category;
using AboHalalBackend.Models;

namespace AboHalalBackend.Services.Category
{
    public interface ICategoryService
    {
        Task<ServiceResponse<GetCategoryDto>> AddCategory(AddCategoryDto categoryDto);
        Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories();
        Task<ServiceResponse<GetCategoryDto>> GetSingleCategory(int id);
    }
}
