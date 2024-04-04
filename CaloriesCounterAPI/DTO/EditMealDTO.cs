using CaloriesCounterAPI.Models;

namespace CaloriesCounterAPI.DTO
{
    public class EditMealDTO
    {
        public int Id { get; set; }

        public MealType Type { get; set; }

        public DateOnly Date { get; set; }
        public List<int>? ProductIds { get; set; }

        public List<int>? AmmoutOfProduct { get; set; }


    }
}
