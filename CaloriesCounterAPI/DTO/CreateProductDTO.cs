using CaloriesCounterAPI.Models;

namespace CaloriesCounterAPI.DTO
{
    /// <summary>
    /// Data transfer object for creating a new product.
    /// </summary>
    public class CreateProductDTO
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the calorie content of the product.
        /// </summary>
        public int Kcal { get; set; }

        /// <summary>
        /// Gets or sets the fat content of the product.
        /// </summary>
        public int Fat { get; set; }

        /// <summary>
        /// Gets or sets the carbohydrates content of the product.
        /// </summary>
        public int Carbs { get; set; }

        /// <summary>
        /// Gets or sets the protein content of the product.
        /// </summary>
        public int Protein { get; set; }
    }
}