using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CaloriesCounterAPI.Models;

namespace CaloriesCounterAPI.Data
{
    public class CaloriesCounterAPIContext : DbContext
    {
        public CaloriesCounterAPIContext (DbContextOptions<CaloriesCounterAPIContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAdded>()
                .HasKey(pm => new { pm.ProductId, pm.MealId });

            modelBuilder.Entity<ProductAdded>()
                .HasOne(pm => pm.Product)
                .WithMany(p => p.ProductAddeds)
                .HasForeignKey(pm => pm.ProductId);

            modelBuilder.Entity<ProductAdded>()
                .HasOne(pm => pm.Meal)
                .WithMany(m => m.ProductsAdded)
                .HasForeignKey(pm => pm.MealId);
        }
        public DbSet<CaloriesCounterAPI.Models.Product> Product { get; set; } = default!;
        public DbSet<CaloriesCounterAPI.Models.ProductAdded> ProductAdded { get; set; } = default!;
        public DbSet<CaloriesCounterAPI.Models.Meal> Meal { get; set; } = default!;
    }
}
