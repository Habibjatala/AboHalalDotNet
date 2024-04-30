using AboHalalBackend.Data;
using AboHalalBackend.Dtos.Product;
using AboHalalBackend.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AboHalalBackend.Services.Product
{

    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProductService(IMapper mapper, DataContext context, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _contextAccessor = contextAccessor;
        }
   



        public async Task<ServiceResponse<ProductDto>> AddProductToCategory([FromBody]ProductDto productDto)
        {
            var response = new ServiceResponse<ProductDto>();

            try
            {
               
              
                var existingCategory = await _context.Categories.Include(c=> c.Products)
                    .FirstOrDefaultAsync(p => p.Id == productDto.CategoryId);
                

                if (existingCategory == null)
                {
                    response.Success = false;
                    response.Message = "Category of this Id does not Exist";
                    return response;
                }
                if (existingCategory.Products.Any(p => p.title == productDto.title))
                {
                    response.Success = false;
                    response.Message = "Category already has a product with the same title";
                    return response;
                }

                var newProduct = new Models.Product
                {
                    title = productDto.title,
                    discount = productDto.discount ?? 0,
                    price = productDto.price ?? 0,
                    description = productDto.description,
                    Picture= productDto.Picture,
                    CategoryId = productDto.CategoryId ?? 0,


                };

                _context.Products.Add(newProduct);
                await _context.SaveChangesAsync();

                var productDtoResponse = _mapper.Map<ProductDto>(newProduct);
                response.Data = productDtoResponse;
                response.Message = "Product added successfully to the category";
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
