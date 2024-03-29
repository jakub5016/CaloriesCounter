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
        public async Task<ActionResult<ProductAddedDTO>> PostProductAdded(int mealId, int productId)
        {
            var meal = await _context.Meal.FindAsync(mealId);
            if (meal == null)
            {
                return NotFound("Meal not found");
            }

            var product = await _context.Product.FindAsync(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            var productAdded = new ProductAdded
            {
                MealId = mealId,
                ProductId = productId
            };

            _context.ProductAdded.Add(productAdded);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest("Failed to update the meal or product. Please try again.");
            }

            var productAddedDTO = new ProductAddedDTO
            {
                Id = productAdded.Id,
                MealId = productAdded.MealId,
                ProductId = productAdded.ProductId
            };

            return CreatedAtAction("GetProductAdded", new { id = productAddedDTO.Id }, productAddedDTO);
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
