using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace CaloriesCounterAPI.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Kcal {  get; set; }
    }
}
