using CaloriesCounterAPI.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CaloriesCounterAPI.Controllers
{
    /// <summary>
    /// Controller for fetching nutrition information from an external API.
    /// </summary>
    [EnableCors("FrontendPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionAPIController : ControllerBase
    {
        private readonly CaloriesCounterAPIContext _context;
        private string APIKEY = Environment.GetEnvironmentVariable("APIKEY");

        public NutritionAPIController(CaloriesCounterAPIContext context)
        {
            _context = context;
        }

        // GET: api/NutritionAPI/nutrition
        /// <summary>
        /// Retrieves nutrition information for a given query.
        /// </summary>
        /// <param name="query">Query string for nutrition information.</param>
        [HttpGet("nutrition")]
        public async Task<IActionResult> GetNutrition(string query)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("X-Api-Key", APIKEY);
                    string url = "https://api.calorieninjas.com/v1/nutrition?query=" + query;

                    // Log request details
                    Console.WriteLine("Sending request to: " + url);

                    HttpResponseMessage response = await client.GetAsync(url);

                    // Log response details
                    Console.WriteLine("Response status code: " + response.StatusCode);
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response body: " + responseBody);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(responseBody); // Return JSON response
                    }
                    else
                    {
                        return BadRequest("Request failed with status code: " + response.StatusCode);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Request exception: " + e.Message);
                return BadRequest("Request exception: " + e.Message);
            }
        }

    }
}
