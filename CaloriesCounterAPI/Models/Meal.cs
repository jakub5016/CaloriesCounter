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

        public List<int>? AmmoutOfProduct { get; set; } 

        public int KcalForMeal { get; set; }

        public bool calculateKcalForMeal() // Tring to calculate kcal for meal, when there is no corresponding ammout return false
        {
            this.KcalForMeal = 0;
            if (this.Products != null)
            {
                foreach (var product in this.Products.Select((value, index) => new {index, value }))
                {
                    if (this.AmmoutOfProduct[product.index] != null)
                    {
                        this.KcalForMeal += product.value.Kcal * this.AmmoutOfProduct[product.index]/100;
                    }

                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
