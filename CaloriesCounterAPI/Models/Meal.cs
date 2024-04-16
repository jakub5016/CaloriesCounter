using System.Text.Json.Serialization;

namespace CaloriesCounterAPI.Models
{
    /// <summary>
    /// Represents the type of meal.
    /// </summary>
    public enum MealType
    {
        Breakfast,
        Dinner,
        Supper
    }

    /// <summary>
    /// Represents a meal in the calorie counter system.
    /// </summary>
    public class Meal
    {
        /// <summary>
        /// Gets or sets the unique identifier of the meal.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the meal (e.g., Breakfast, Dinner, Supper).
        /// </summary>
        public MealType Type { get; set; }
        
        /// <summary>
        /// Gets or sets the date of the meal.
        /// </summary>
        public DateOnly Date { get; set; }
        
        /// <summary>
        /// Gets or sets the list of products included in the meal.
        /// </summary>
        public virtual ICollection<Product>? Products { get; set; }

        /// <summary>
        /// Gets or sets the amount of each product in the meal.
        /// </summary>
        public List<int>? AmountOfProduct { get; set; } 

        /// <summary>
        /// Gets or sets the total calories for the meal.
        /// </summary>
        public int KcalForMeal { get; set; }

        /// <summary>
        /// Gets or sets the total fat content for the meal.
        /// </summary>
        public int FatForMeal { get; set; }

        /// <summary>
        /// Gets or sets the total carbohydrates content for the meal.
        /// </summary>
        public int CarbsForMeal { get; set; }

        /// <summary>
        /// Gets or sets the total protein content for the meal.
        /// </summary>
        public int ProteinForMeal { get; set; }

        /// <summary>
        /// Calculates the total nutritional values for the meal.
        /// </summary>
        /// <returns>True if calculation was successful, otherwise false.</returns>
        public bool CalculateKcalForMeal()
        {
            this.KcalForMeal = 0;
            this.FatForMeal = 0;
            this.CarbsForMeal = 0;
            this.ProteinForMeal = 0;
            
            if (Products != null && AmountOfProduct != null && Products.Count == AmountOfProduct.Count)
            {
                foreach (var product in this.Products.Select((value, index) => new {index, value }))
                {
                    this.KcalForMeal += product.value.Kcal * this.AmountOfProduct[product.index] / 100;
                    this.FatForMeal += product.value.Fat * this.AmountOfProduct[product.index] / 100;
                    this.CarbsForMeal += product.value.Carbs * this.AmountOfProduct[product.index] / 100;
                    this.ProteinForMeal += product.value.Protein * this.AmountOfProduct[product.index] / 100;   
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
