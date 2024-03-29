namespace CaloriesCounterAPI.Models
{
    public class ProductAdded
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public int ProductId { get; set; }

        public virtual Meal Meal { get; set; }
        public virtual Product Product { get; set; }
    }
}
