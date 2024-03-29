namespace CaloriesCounterAPI.Models
{
    public class MealDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<ProductAddedDTO> ProductsAdded { get; set; }
    }
}
