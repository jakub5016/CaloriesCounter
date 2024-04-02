using System.Text.Json.Serialization;

namespace CaloriesCounterAPI.Models
{
    public enum MealType
    {
        Breakfast,
        Dinner,
        Supper
    }
    public class Meal
    {
        public int Id { get; set; }

        public MealType Type { get; set; }
            
        public DateOnly Date { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
