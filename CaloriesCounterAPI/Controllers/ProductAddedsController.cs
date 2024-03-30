using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaloriesCounterAPI.Data;
using CaloriesCounterAPI.Models;
using Microsoft.CodeAnalysis;
using System.Net;

namespace CaloriesCounterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAddedsController : ControllerBase
    {
        private readonly CaloriesCounterAPIContext _context;

        public ProductAddedsController(CaloriesCounterAPIContext context)
        {
            _context = context;
        }

        // GET: api/ProductAddeds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductAdded>>> GetProductAdded()
        {
            return await _context.ProductAdded.ToListAsync();
        }

        // GET: api/ProductAddeds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductAdded>> GetProductAdded(int id)
        {
            var productAdded = await _context.ProductAdded.FindAsync(id);

            if (productAdded == null)
            {
                return NotFound();
            }

            return productAdded;
        }

        // PUT: api/ProductAddeds/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductAdded(int id, ProductAdded productAdded)
        {
            if (id != productAdded.Id)
            {
                return BadRequest();
            }

            _context.Entry(productAdded).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAddedExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductAddeds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductAdded>> PostProductAdded(int mealId, int productId)
        {
            var product = await _context.Product.FindAsync(productId);
            var meal = await _context.Meal.FindAsync(mealId);

            if (product == null || meal == null)
            {
                return BadRequest("Invalid product ID or meal ID.");
            }

            // Assuming you have a junction table named ProductMeal
            var productMeal = new ProductAdded
            {
                ProductId = productId,
                MealId = mealId
            };

            _context.ProductAdded.Add(productMeal);
            await _context.SaveChangesAsync();

            return Ok("Product added to meal successfully.");
        }




        // DELETE: api/ProductAddeds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAdded(int id)
        {
            var productAdded = await _context.ProductAdded.FindAsync(id);
            if (productAdded == null)
            {
                return NotFound();
            }

            _context.ProductAdded.Remove(productAdded);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductAddedExists(int id)
        {
            return _context.ProductAdded.Any(e => e.Id == id);
        }
    }
}
