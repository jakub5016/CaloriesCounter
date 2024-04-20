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
using Xunit;
using Assert = Xunit.Assert;

namespace CaloriesCounterAPI.Test.Controllers;

[TestClass]
[TestSubject(typeof(ProductController))]
public class ProductControlerTest
{

    [Fact]
    public async Task GetAllProducts_ReturnsListOfProducts()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_ProductsDatabase")
            .Options;
        using (var context = new CaloriesCounterAPIContext(options))
        {
            context.Product.AddRange(new List<Product>
            {
                new Product { ID = 1, Name = "Apple" },
                new Product { ID = 2, Name = "Banana" }
            });
            context.SaveChanges();
        }

        // Act
        using (var context = new CaloriesCounterAPIContext(options))
        {
            var controller = new ProductController(context);
            var result = await controller.GetAllProducts();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(actionResult.Value);
            Assert.Equal(2, model.Count());
        }
    }

    [Fact]
    public async Task GetProductById_ReturnsProductById()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_ProductsDatabase")
            .Options;
        using (var context = new CaloriesCounterAPIContext(options))
        {
            context.Product.Add(new Product { ID = 1, Name = "Apple" });
            context.SaveChanges();
        }

        // Act
        using (var context = new CaloriesCounterAPIContext(options))
        {
            var controller = new ProductController(context);
            var result = await controller.GetProductById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Product>>(result);
            Assert.NotNull(actionResult.Value);
            Assert.Equal("Apple", actionResult.Value.Name);
        }
    }

    [Fact]
    public async Task PostNewProduct_AddsNewProduct()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_ProductsDatabase")
            .Options;

        // Act
        using (var context = new CaloriesCounterAPIContext(options))
        {
            var controller = new ProductController(context);
            var result = await controller.PostNewProduct(new CreateProductDTO
            {
                Name = "Orange",
                Kcal = 50,
                Fat = 1,
                Carbs = 12,
                Protein = 1
            });

            // Assert
            var actionResult = Assert.IsType<OkResult>(result);
            using (var contextAfter = new CaloriesCounterAPIContext(options))
            {
                Assert.Equal(1, contextAfter.Product.Count());
                var product = contextAfter.Product.Single();
                Assert.Equal("Orange", product.Name);
                Assert.Equal(50, product.Kcal);
                Assert.Equal(0.3, product.Fat);
                Assert.Equal(12, product.Carbs);
                Assert.Equal(0.9, product.Protein);
            }
        }
    }

    [Fact]
    public async Task SearchProductByName_ReturnsListOfMatchingProducts()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_ProductsDatabase")
            .Options;
        using (var context = new CaloriesCounterAPIContext(options))
        {
            context.Product.AddRange(new List<Product>
            {
                new Product { ID = 1, Name = "Apple" },
                new Product { ID = 2, Name = "Banana" },
                new Product { ID = 3, Name = "Orange" }
            });
            context.SaveChanges();
        }

        // Act
        using (var context = new CaloriesCounterAPIContext(options))
        {
            var controller = new ProductController(context);
            var result = await controller.SearchProductByName("an");

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Product>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(actionResult.Value);
            Assert.Equal(2, model.Count());
        }
    }

    [Fact]
    public async Task DeleteProductById_DeletesProduct()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_ProductsDatabase")
            .Options;
        using (var context = new CaloriesCounterAPIContext(options))
        {
            context.Product.Add(new Product { ID = 1, Name = "Apple" });
            context.SaveChanges();
        }

        // Act
        using (var context = new CaloriesCounterAPIContext(options))
        {
            var controller = new ProductController(context);
            var result = await controller.DeleteProductById(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
            using (var contextAfter = new CaloriesCounterAPIContext(options))
            {
                Assert.Empty(contextAfter.Product);
            }
        }
    }
}