using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repository.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {//refactoring: var products = await shopOnlineDbContext.Products
                                               //.Include(p => p.ProductCategory).ToListAsync();
                var products = await this.productRepository.GetItems();
                var productCategories = await this.productRepository.GetCategories(); 

                if (products == null ||  productCategories == null) 
                {
                    return NotFound();
                }
                else
                {
                    var productDtos = products.ConvertToDo(productCategories);
                    return Ok(productDtos);

                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieviing data from the database");
            }

        }

    }
}
