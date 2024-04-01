using CaloriesCounterAPI.Models;

namespace CaloriesCounterAPI.DTO
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public int Kcal { get; set; }

        public int Fat { get; set; }

        public int Carbs { get; set; }

        public int Protein { get; set; }
        public List<int>? MealIDs { get; set; }
    }
}
