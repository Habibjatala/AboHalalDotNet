using AboHalalBackend.Dtos.Product;
using AboHalalBackend.FilesServices;
using AboHalalBackend.Models;
using AboHalalBackend.Services.Product;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AboHalalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        public ProductController(IProductService productService, IFileService fileService)
        {
            _productService = productService;
            _fileService = fileService;
        }

     
        [HttpPost("CreateProduct")]

        
        public async Task<IActionResult> AddProduct([FromForm] ProductDto productDto)
        {
            if (productDto.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(productDto.ImageFile);
                if (fileResult.Item1 == 1)
                {
                    productDto.Picture = fileResult.Item2;
                }
            }


            var response = await _productService.AddProductToCategory( productDto);

            if (response.Success)
                return Ok(response.Data);

            return BadRequest(response.Message);
        }
    }
}
