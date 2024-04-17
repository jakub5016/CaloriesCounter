using CaloriesCounterAPI.Data;
using CaloriesCounterAPI.DTO;
using CaloriesCounterAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaloriesCounterAPI.Controllers
{
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
        [HttpGet]
        public async Task<ActionResult<User>> GetUser()
        {
            return await _context.User.FirstOrDefaultAsync();
        }

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

    }
}
