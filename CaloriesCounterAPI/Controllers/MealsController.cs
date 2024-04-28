using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaloriesCounterAPI.Data;
using CaloriesCounterAPI.Models;
using CaloriesCounterAPI.DTO;
using Microsoft.AspNetCore.Cors;

namespace CaloriesCounterAPI.Controllers
{
    /// <summary>
    /// Controller for managing meal-related operations.
    /// </summary>
    [EnableCors("FrontendPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly CaloriesCounterAPIContext _context;

        public MealsController(CaloriesCounterAPIContext context)
        {
            _context = context;
        }

        // GET: api/Meals
        /// <summary>
        /// Retrieves all meals.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeal()
        {
            return await _context.Meal.Include(m => m.Products).ToListAsync();
        }

        // GET: api/Meals/5
        /// <summary>
        /// Retrieves meals by date.
        /// </summary>
        /// <param name="Date">Date to filter meals.</param>
        [HttpGet("{Date}")]
        public async Task<ActionResult<List<Meal>>> GetMealsByDate(DateOnly Date)
        {
            var mealList = await _context.Meal.Where(m => m.Date.CompareTo(Date) == 0)
                .Include(m => m.Products).OrderBy(m => m.Type).ToListAsync();

            if (mealList == null)
            {
                return NotFound();
            }

            return mealList;
        }

        // PUT: api/Meals/5
        /// <summary>
        /// Modifies a meal.
        /// </summary>
        /// <param name="id">Identifier of the meal to modify.</param>
        /// <param name="mealDTO">Data for modifying the meal.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(int id, EditMealDTO mealDTO)
        {
            var meal = await _context.Meal.Include(m => m.Products).FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NoContent();
            }
            meal.Type = mealDTO.Type;
            meal.AmountOfProduct = mealDTO.AmountOfProduct;
            meal.Date = mealDTO.Date;
            if (meal.Products != null)
            {
                meal.Products.Clear();

            }

            foreach (var index in mealDTO.ProductIds)
            {
                var product = await _context.Product.FindAsync(index);

                if (product == null)
                {
                    return NoContent();
                }
                if (meal.Products != null)
                {
                    meal.Products.Add(product);
                }
                else
                {
                    meal.Products = new List<Product>([product]);
                }
            }

            if (!meal.CalculateKcalForMeal())
            {
                return NoContent();
            };

            _context.Entry(meal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Meal successfully modified");
        }

        // POST: api/Meals
        /// <summary>
        /// Creates a new meal.
        /// </summary>
        /// <param name="mealDTO">Data for creating the meal.</param>
        [HttpPost]
        public async Task<ActionResult<Meal>> PostMeal(CreateMealDTO mealDTO)
        {
            var meal = new Meal
            {
                Type = mealDTO.Type,
                AmountOfProduct = mealDTO.AmountOfProduct,
                Date = mealDTO.Date
            };

            if (mealDTO.ProductIds != null)
            {
                meal.Products = new List<Product>();
                foreach (var id in mealDTO.ProductIds)
                {
                    var newProduct = _context.Product.Find(id);
                    if (newProduct != null)
                    {
                        meal.Products.Add(newProduct);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
            }

            if (!meal.CalculateKcalForMeal())
            {
                return NoContent();
            };

            _context.Meal.Add(meal);
            await _context.SaveChangesAsync();

            return Ok(await _context.Meal.Include(m => m.Products).ToListAsync());
        }

        // DELETE: api/Meals/5
        /// <summary>
        /// Deletes a meal.
        /// </summary>
        /// <param name="id">Identifier of the meal to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var meal = await _context.Meal.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            _context.Meal.Remove(meal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Meals/{mealId}/Products/{productId}
        /// <summary>
        /// Deletes a product from a meal.
        /// </summary>
        /// <param name="mealId">Identifier of the meal.</param>
        /// <param name="productId">Identifier of the product to delete.</param>
        [HttpDelete("{mealId}/Products/{productId}")]
        public async Task<IActionResult> DeleteProductFromMeal(int mealId, int productId)
        {
            var meal = await _context.Meal.Include(m => m.Products).FirstOrDefaultAsync(m => m.Id == mealId);
            if (meal == null)
            {
                return NotFound("Meal not found");
            }
            if (meal.Products == null)
            {
                return NotFound("There is no Product in this meal");
            }
            var product = meal.Products.FirstOrDefault(p => p.ID == productId);
            var productList = meal.Products.ToList();
            var index = productList.FindIndex(p => p.ID == productId);
            if (product == null)
            {
                return NotFound("Product not found in the meal");
            }
            if (meal.AmountOfProduct != null)
            {
                meal.AmountOfProduct.RemoveAt(index);
            }
            meal.Products.Remove(product);
            meal.CalculateKcalForMeal();
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PATCH: api/Meals/{id}/AppendProduct/{productId}/{quantity}
        /// <summary>
        /// Appends a product to a meal.
        /// </summary>
        /// <param name="id">Identifier of the meal.</param>
        /// <param name="productId">Identifier of the product to append.</param>
        /// <param name="quantity">Quantity of the product to append.</param>
        [HttpPatch("{id}/AppendProduct/{productId}/{quantity}")]
        public async Task<IActionResult> AppendProductToMeal(int id, int productId, int quantity)
        {
            var meal = await _context.Meal.Include(m => m.Products).FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound("Meal not found");
            }

            var product = await _context.Product.FindAsync(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            if (meal.Products.FirstOrDefault(e => e.ID == productId) != null)
            {
                var i = 0;
                foreach (var p in meal.Products)
                {
                    if (p.ID == productId)
                    {
                        meal.AmountOfProduct[i] = quantity;
                    }
                    i++;
                }
            }
            else
            {
                meal.Products.Add(product);
                if (meal.AmountOfProduct == null)
                {
                    meal.AmountOfProduct = new List<int>();
                }
                meal.AmountOfProduct.Add(quantity);
            }
            if (!meal.CalculateKcalForMeal())
            {
                return NoContent();
            }

            await _context.SaveChangesAsync();
            return Ok("Product successfully appended to the meal");
        }
        /// <summary>
        /// Checks if this Meal exisist based on id
        /// </summary>
        /// <param name="id">Identifier of the meal.</param>
        private bool MealExists(int id)
        {
            return _context.Meal.Any(e => e.Id == id);
        }
    }
}
