using CaloriesCounterAPI.Data;
using CaloriesCounterAPI.DTO;
using CaloriesCounterAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaloriesCounterAPI.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CaloriesCounterAPIContext _context;

        public UserController(CaloriesCounterAPIContext context)
        {
            _context = context;
        }

        // GET: api/User
        /// <summary>
        /// Retrieves the user.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<User>> GetUser()
        {
            return await _context.User.FirstOrDefaultAsync();
        }
        // POST: api/User
        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="userGot">Data for creating the user.</param>
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDTO userGot)
        {
            var user = new User
            {
                Weight = userGot.Weight,
                Gender = userGot.Gender,
                Age = userGot.Age,
                Height = userGot.Height,
            };

            _context.User.Add(user);
            user.calculateMaintainceAndBmi();

            _context.SaveChanges();

            return Ok();
        }
        // DELETE: api/User/{id}
        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">Identifier of the user to delete.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsertById(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
