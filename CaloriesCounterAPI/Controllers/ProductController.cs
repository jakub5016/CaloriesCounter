using CaloriesCounterAPI.Data;
using CaloriesCounterAPI.DTO;
using CaloriesCounterAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaloriesCounterAPI.Controllers
{
    [EnableCors("FrontendPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductControler : ControllerBase
    {

        private readonly CaloriesCounterAPIContext _context;

        public ProductControler(CaloriesCounterAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {

            return await _context.Product.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = _context.Product.Find(id);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]

        public async Task<ActionResult<Product>> PostNewProduct(CreateProductDTO productDTO)
        {
            var newProduct = new Product
            {
                Protein = productDTO.Protein,
                Fat = productDTO.Fat,
                Carbs = productDTO.Carbs,
                Name = productDTO.Name,
                Kcal = productDTO.Kcal
            };

            _context.Product.Add(newProduct);
            await _context.SaveChangesAsync();
            return Ok();
        }
        //wyszukiwanie po nazwie
        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProductByName(String search)
        {
            var products = await _context.Product
                .Where(p => p.Name.ToLower().Contains(search.ToLower()))
                .ToListAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound();
            }

            return products;
        }
    }
}
