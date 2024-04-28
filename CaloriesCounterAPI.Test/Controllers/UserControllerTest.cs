using CaloriesCounterAPI.Controllers;
using CaloriesCounterAPI.Data;
using CaloriesCounterAPI.Models;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = Xunit.Assert;

namespace CaloriesCounterAPI.Test.Controllers
{
    [TestClass]
    [TestSubject(typeof(User))]
    public class UserControllerTest
    {
        [TestMethod]
        public async Task GetUser_WhenThereIsTwo()
        {
            var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_Users")
            .Options;

            using (var context = new CaloriesCounterAPIContext(options))
            {
                context.Database.EnsureDeleted();
                var users = new List<User>([
                    new User { Id = 1, Age = 20, Height=170, Weight= 70},
                    new User { Id = 2, Age = 60, Height=190, Weight= 90}
                ]);
                context.User.AddRange(users);
                context.SaveChanges();

                var controller = new UserController(context);
                var result = await controller.GetUser();

                var actionResult = Assert.IsType<ActionResult<User>>(result);
                Assert.Equal(1, actionResult.Value.Id);

            }

            
        }

        [TestMethod]
        public async Task GetUser_WhenThereIsOne()
        {
            var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_Users")
            .Options;

            using (var context = new CaloriesCounterAPIContext(options))
            {
                context.Database.EnsureDeleted();
                var users = new List<User>([new User { Id = 1, Age = 20, Height=170, Weight= 70}]);
                context.User.AddRange(users);
                context.SaveChanges();

                var controller = new UserController(context);
                var result = await controller.GetUser();

                var actionResult = Assert.IsType<ActionResult<User>>(result);
                Assert.Equal(1, actionResult.Value.Id);

            }


        }

        [TestMethod]
        public async Task GetUser_WhenThereIsNone()
        {
            var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_Users")
            .Options;

            using (var context = new CaloriesCounterAPIContext(options))
            {
                context.Database.EnsureDeleted();

                var controller = new UserController(context);
                var result = await controller.GetUser();

                var actionResult = Assert.IsType<ActionResult<User>>(result);
                Assert.Null(actionResult.Value);

            }


        }

        [TestMethod]
        public async Task CreateUser()
        {
            var options = new DbContextOptionsBuilder<CaloriesCounterAPIContext>()
            .UseInMemoryDatabase(databaseName: "Test_Users")
            .Options;

            using (var context = new CaloriesCounterAPIContext(options))
            {
                context.Database.EnsureDeleted();

                var controller = new UserController(context);
                await controller.CreateUser(new DTO.UserDTO { Age = 15, Gender = true, Height = 120, Weight = 30});

                var userCreated =  context.User.FirstOrDefault();
                Assert.NotNull(userCreated);
                Assert.Equal(15, userCreated.Age);
                Assert.True(userCreated.Gender);
                Assert.Equal(120, userCreated.Height);
                Assert.Equal(30, userCreated.Weight);

            }


        }
    }
}
