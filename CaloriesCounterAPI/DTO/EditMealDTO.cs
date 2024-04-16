using CaloriesCounterAPI.Models;
using System;
using System.Collections.Generic;

namespace CaloriesCounterAPI.DTO
{
    /// <summary>
    /// Data transfer object for editing a meal.
    /// </summary>
    public class EditMealDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the meal.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the meal.
        /// </summary>
        public MealType Type { get; set; }

        /// <summary>
        /// Gets or sets the date of the meal.
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Gets or sets the IDs of the products to be included in the meal.
        /// </summary>
        public List<int>? ProductIds { get; set; }

        /// <summary>
        /// Gets or sets the amounts of each product to be included in the meal.
        /// </summary>
        public List<int>? AmountOfProduct { get; set; }
    }
}