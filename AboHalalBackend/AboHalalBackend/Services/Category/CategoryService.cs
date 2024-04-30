using AboHalalBackend.Data;
using AboHalalBackend.Dtos.Category;
using AboHalalBackend.Dtos.Product;
using AboHalalBackend.Models;

using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AboHalalBackend.Services.Category
{
    public class CategoryService: ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public CategoryService(IMapper mapper, DataContext context, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<ServiceResponse<GetCategoryDto>> AddCategory(AddCategoryDto categoryDto)
        {
            var serviceResponse = new ServiceResponse<GetCategoryDto>();
            try
            {
                var existingCategory = _context.Categories.FirstOrDefault(c => c.CategoryName == categoryDto.CategoryName);

            if (existingCategory != null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Category with the same name already exists";
                return serviceResponse;
            }

            var newCategory = new Models.Category
            {
                CategoryName = categoryDto.CategoryName,
              


            };

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
                var getCategoryDto = new GetCategoryDto
                {
                    Id = newCategory.Id,
                    CategoryName = newCategory.CategoryName,
                    Products = _mapper.Map<ICollection<ProductDto>>(newCategory.Products)
                };
                serviceResponse.Data = getCategoryDto;
            serviceResponse.Message = "Category added successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error: {ex.Message}";
                
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            var serviceResponse = new ServiceResponse<List<GetCategoryDto>>();

            try
            {
                var categories = await _context.Categories
                    .Include(c => c.Products) // Include products if you want to retrieve them
                    .ToListAsync();

                var categoryDtos = categories.Select(c => _mapper.Map<GetCategoryDto>(c)).ToList(); // _mapper.Map<List<GetCategoryDto>>(categories);

                serviceResponse.Data = categoryDtos;
                serviceResponse.Message = "Categories retrieved successfully";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Error: {ex.Message}";
               
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetSingleCategory(int id)
        {
            var response = new ServiceResponse<GetCategoryDto>();

            try
            {
                var category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);

                if (category == null)
                {
                    response.Success = false;
                    response.Message = "Category does not exist for this ID";
                }
                else
                {
                    var categoryDto = _mapper.Map<GetCategoryDto>(category);
                    response.Data = categoryDto;
                    response.Message = "Category retrieved successfully";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error: {ex.Message}";
                
            }

            return response;
        }

    }
}
