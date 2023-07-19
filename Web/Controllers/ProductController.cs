using Domain.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Product;
using Service.Interfaces;

namespace Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(List<ProductDto> products)
        {
            try
            {
                await _productService.PostProduct(products);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
