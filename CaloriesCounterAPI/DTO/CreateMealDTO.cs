using CaloriesCounterAPI.Models;

namespace CaloriesCounterAPI.DTO
{
    public class CreateMealDTO
    {
        public MealType Type { get; set; }

        public DateOnly Date { get; set; }

        public List<int>? ProductIds { get; set; }

        public List<int>? AmmoutOfProduct { get; set; }
    }
}
