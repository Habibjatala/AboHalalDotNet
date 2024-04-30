using AboHalalBackend.Data;
using AboHalalBackend.Dtos.Category;
using AboHalalBackend.Services.Category;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AboHalalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDto categoryDto)
        {
            var serviceResponse = await _categoryService.AddCategory(categoryDto);

            if (serviceResponse.Success)
                return Ok(serviceResponse.Data);

            return BadRequest(serviceResponse.Message);
        }

        [HttpGet]

        public async Task<IActionResult> GetAllCategories()
        {
            var res = await _categoryService.GetAllCategories();
            if (res.Success)
            {
                return Ok(res.Data);
            }
            return BadRequest(res.Message);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleCategory(int  id)
        {
            var res = await _categoryService.GetSingleCategory(id);
            if (res.Success)
            {
                return Ok(res.Data);
            }
            return BadRequest(res.Message);
        }

    }
}
