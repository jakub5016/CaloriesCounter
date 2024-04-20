using CaloriesCounterAPI.Data;
using CaloriesCounterAPI.DTO;
using CaloriesCounterAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaloriesCounterAPI.Controllers
{
    /// <summary>
    /// Controller for managing product-related operations.
    /// </summary>
    [EnableCors("FrontendPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CaloriesCounterAPIContext _context;

        public ProductController(CaloriesCounterAPIContext context)
        {
            _context = context;
        }

        // GET: api/Product
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            return await _context.Product.ToListAsync();
        }

        // GET: api/Product/{id}
        /// <summary>
        /// Retrieves a product by ID.
        /// </summary>
        /// <param name="id">Identifier of the product.</param>
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

        // POST: api/Product
        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="productDTO">Data for creating the product.</param>
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

        // POST: api/Product/search
        /// <summary>
        /// Searches for products by name.
        /// </summary>
        /// <param name="search">Search query string.</param>
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

        // DELETE: api/Product/{id}
        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">Identifier of the product to delete.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductById(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
