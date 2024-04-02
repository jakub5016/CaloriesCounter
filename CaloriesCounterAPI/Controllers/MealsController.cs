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

namespace CaloriesCounterAPI.Controllers
{
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeal()
        {
            return await _context.Meal.Include(m => m.Products).ToListAsync();
        }

        // GET: api/Meals/5
        [HttpGet("{Date}")]
        public async Task<ActionResult<List<Meal>>> GetMealsByDate(DateOnly Date)
        {
            var mealList = await _context.Meal.Where(m => m.Date.CompareTo(Date) == 0).Include(m=> m.Products).ToListAsync();

            if (mealList == null)
            {
                return NotFound();
            }

            return mealList;
        }

        // PUT: api/Meals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(int id, EditMealDTO mealDTO)
        {
            var meal = await _context.Meal.Include(m => m.Products).FirstOrDefaultAsync(m => m.Id == id);
            meal.Type = mealDTO.Type;
            meal.Date = mealDTO.Date;
            meal.Products.Clear();


            foreach (var index in mealDTO.ProductIds)
            {
                var product = await _context.Product.FindAsync(index);

                if (product == null) 
                { 
                    return NoContent();
                }

                meal.Products.Add(product);
            }

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

            return Ok("Meal succesfully modified");
        }

        // POST: api/Meals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Meal>> PostMeal(CreateMealDTO mealDTO)
        {
            var meal = new Meal
            {
                Type = mealDTO.Type,
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
                        return NoContent() ;
                    }
                }
            }

            _context.Meal.Add(meal);
            await _context.SaveChangesAsync();

            return Ok(await _context.Meal.Include(m => m.Products).ToListAsync());
        }

        // DELETE: api/Meals/5
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

        private bool MealExists(int id)
        {
            return _context.Meal.Any(e => e.Id == id);
        }
    }
}
