using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CaloriesCounterAPI.Models;

namespace CaloriesCounterAPI.Data
{
    /// <summary>
    /// Represents the database context for the CaloriesCounterAPI.
    /// </summary>
    public class CaloriesCounterAPIContext : DbContext
    {
        public CaloriesCounterAPIContext(DbContextOptions<CaloriesCounterAPIContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for products.
        /// </summary>
        public DbSet<Product> Product { get; set; } = default!;

        /// <summary>
        /// Gets or sets the DbSet for meals.
        /// </summary>
        public DbSet<Meal> Meal { get; set; } = default!;
    }
}