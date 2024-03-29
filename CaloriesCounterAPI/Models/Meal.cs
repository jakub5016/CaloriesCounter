namespace CaloriesCounterAPI.Models
{
    public enum MealTypes
    {
        Breakfast,
        Dinner,
        Supper
    }
    public class Meal
    {
        public int Id { get; set; }

        public MealTypes Type { get; set; }

        public virtual ICollection<ProductAdded>? ProductsAdded { get; set; }
    }
}
