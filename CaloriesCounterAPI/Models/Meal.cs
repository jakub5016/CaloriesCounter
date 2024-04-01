using System.Text.Json.Serialization;

namespace CaloriesCounterAPI.Models
{
    public class Meal
    {
        public int Id { get; set; }

        public string Type { get; set; }
            
        public DateOnly Date { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
