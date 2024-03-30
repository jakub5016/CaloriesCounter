namespace CaloriesCounterAPI.Models
{
    public class ProductAdded
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
