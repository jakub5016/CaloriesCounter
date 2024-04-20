using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaloriesCounterAPI.Controllers;
using CaloriesCounterAPI.Data;
using CaloriesCounterAPI.DTO;
using CaloriesCounterAPI.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace CaloriesCounterAPI.Test.Controllers;

[TestClass]
[TestSubject(typeof(MealsController))]
public class MealsControllerTest
{
    [TestMethod]
    public void CalculateKcalForMeal()
    {
        // Arrange
        var meal = GetTestProducts();

            // Act
            var result = meal[0];
            
        // Assert
        Assert.True(result.CalculateKcalForMeal());
        Assert.Equal(136, result.KcalForMeal);
        Assert.Equal(4, result.FatForMeal);
        Assert.Equal(24, result.CarbsForMeal);
        Assert.Equal(4, result.ProteinForMeal);
    }
    [TestMethod]
    public async Task GetMeal_ReturnsListOfMeals()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_MealsDatabase2")
            .Options;
        using (var context = new CaloriesCounterAPIContext(options))
        {
            context.Meal.AddRange(GetTestProducts());
            context.SaveChanges();
        }

        // Act
        using (var context = new CaloriesCounterAPIContext(options))
        {
            var controller = new MealsController(context);
            var result = await controller.GetMeal();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Meal>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Meal>>(actionResult.Value);
            Assert.Equal(4, model.Count());
        }
    }
    [TestMethod]
    public async Task GetMealsByDate_ReturnsListOfMealsForSpecificDate()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_MealsDatabase")
            .Options;
        using (var context = new CaloriesCounterAPIContext(options))
        {
           context.Meal.AddRange(GetTestProducts());
            context.SaveChanges();
        }

        // Act
        using (var context = new CaloriesCounterAPIContext(options))
        {
            var controller = new MealsController(context);
            var result = await controller.GetMealsByDate(new DateOnly(2024, 4, 19));

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Meal>>>(result);
            var model = Assert.IsAssignableFrom<List<Meal>>(actionResult.Value);
            Assert.Equal(2, model.Count());
        }
    }
    [TestMethod]
    public async Task DeleteMeal_ExistingMeal_ReturnsNoContent()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_MealsDatabase")
            .Options;
        using (var context = new CaloriesCounterAPIContext(options))
        {
            context.Meal.Add(new Meal { Id = 1 });
            context.SaveChanges();
        }

        // Act
        using (var context = new CaloriesCounterAPIContext(options))
        {
            var controller = new MealsController(context);
            var result = await controller.DeleteMeal(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
    [TestMethod]
    public async Task AppendProductToMeal_ExistingProductInMeal_ReturnsOk()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_MealsDatabase")
            .Options;
        using (var context = new CaloriesCounterAPIContext(options))
        {
            var meal = new Meal
            {
                Id = 10,
                Type = MealType.Supper,
                Date = new DateOnly(2024, 4, 21),
                Products = new List<Product>
                {
                    new Product { Name = "Grilled Salmon", Kcal = 210, Fat = 12, Carbs = 0, Protein = 23 },
                    new Product { Name = "Quinoa Salad", Kcal = 180, Fat = 6, Carbs = 30, Protein = 5 },
                    new Product { Name = "Avocado", Kcal = 160, Fat = 15, Carbs = 9, Protein = 2 }
                },
                AmountOfProduct = new List<int> { 150, 100, 50 }
            };
        context.Meal.Add(meal);
            context.SaveChanges();
        }

        // Act
        using (var context = new CaloriesCounterAPIContext(options))
        {
            var controller = new MealsController(context);
            var result = await controller.AppendProductToMeal(10, 1, 100);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
    private List<Meal> GetTestProducts()
    {
        var testProducts = new List<Meal>();
        testProducts.Add(new Meal
        {
            Id = 1,
            Type = MealType.Breakfast,
            Date = new DateOnly(2024, 4, 19),
            Products = new List<Product>
            {
                new Product { Name = "Oatmeal", Kcal = 68, Fat = 2, Carbs = 12, Protein = 2 },
                new Product { Name = "Banana", Kcal = 89, Fat = 1, Carbs = 23, Protein = 1 }
            },
            AmountOfProduct = new List<int> { 200, 1 }
        });
        testProducts.Add(new Meal()
        {
            Id = 2,
            Type = MealType.Dinner,
            Date = new DateOnly(2024, 4, 19),
            Products = new List<Product>
            {
                new Product { Name = "Grilled Chicken Breast", Kcal = 165, Fat = 4, Carbs = 0, Protein = 31 },
                new Product { Name = "Steamed Broccoli", Kcal = 55, Fat = 1, Carbs = 11, Protein = 4 },
                new Product { Name = "Brown Rice", Kcal = 111, Fat = 1, Carbs = 24, Protein = 3 }
            },
            AmountOfProduct = new List<int> { 150, 100, 150 }
        });
        testProducts.Add(new Meal
        {
            Id = 3,
            Type = MealType.Supper,
            Date = new DateOnly(2024, 4, 20),
            Products = new List<Product>
            {
                new Product { Name = "Salmon Fillet", Kcal = 208, Fat = 13, Carbs = 0, Protein = 22 },
                new Product { Name = "Quinoa", Kcal = 120, Fat = 2, Carbs = 21, Protein = 4 },
                new Product { Name = "Steamed Asparagus", Kcal = 20, Fat = 1, Carbs = 4, Protein = 2 }
            },
            AmountOfProduct = new List<int> { 150, 100, 100 }
        });
        testProducts.Add(new Meal
        {
            Id = 4,
            Type = MealType.Breakfast,
            Date = new DateOnly(2024, 4, 20),
            Products = new List<Product>
            {
                new Product { Name = "Scrambled Eggs", Kcal = 155, Fat = 11, Carbs = 1, Protein = 13 },
                new Product { Name = "Whole Wheat Toast", Kcal = 70, Fat = 1, Carbs = 12, Protein = 3 },
                new Product { Name = "Spinach", Kcal = 23, Fat = 1, Carbs = 4, Protein = 3 }
            },
            AmountOfProduct = new List<int> { 200, 2, 50 }
        });
        
        return testProducts;
    }
}

