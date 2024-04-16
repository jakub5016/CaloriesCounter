using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json.Serialization;

namespace CaloriesCounterAPI.Models
{
    /// <summary>
    /// Represents a product in the calorie counter system.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the calorie content of the product.
        /// </summary>
        public int Kcal {  get; set; }

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

        /// <summary>
        /// Gets or sets the meals associated with the product.
        /// </summary>
        [JsonIgnore]
        public ICollection<Meal> Meals { get; set; }
    }
}