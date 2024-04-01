using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json.Serialization;
namespace CaloriesCounterAPI.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Kcal {  get; set; }

        public int Fat { get; set; }

        public int Carbs { get; set; }

        public int Protein { get; set; }
        [JsonIgnore]
        public ICollection<Meal> Meals { get; set; }
    }
}
