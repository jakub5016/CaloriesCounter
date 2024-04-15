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
        public int FatForMeal { get; set; }
        public int CarbsForMeal { get; set; }
        public int ProteinForMeal { get; set; }


        public bool CalculateKcalForMeal() // Tring to calculate kcal for meal, when there is no corresponding ammout return false
        {
            this.KcalForMeal = 0;
            this.FatForMeal = 0;
            this.CarbsForMeal = 0;
            this.ProteinForMeal = 0;
            
            if (Products != null && AmmoutOfProduct != null && Products.Count == AmmoutOfProduct.Count)
            {
                foreach (var product in this.Products.Select((value, index) => new {index, value }))
                {
                    this.KcalForMeal += product.value.Kcal * this.AmmoutOfProduct[product.index]/100;
                    this.FatForMeal += product.value.Fat * this.AmmoutOfProduct[product.index]/100;
                    this.CarbsForMeal += product.value.Carbs * this.AmmoutOfProduct[product.index]/100;
                    this.ProteinForMeal += product.value.Protein * this.AmmoutOfProduct[product.index]/100;
                        
                }
            }

            return true;
        }

    }
}
