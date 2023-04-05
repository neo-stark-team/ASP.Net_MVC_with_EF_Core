using NUnit.Framework;
using UserProject.Controllers;
using UserProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Linq;

namespace TestUser
{
    public class UserControllerTests
    {
        private UserDbContext _context;
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            // create an in-memory database for testing
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;
            _context = new UserDbContext(options);

            // seed the database with test data
            _context.Users.Add(new User { Id = 101, Name = "John Doe", Email = "john@example.com" });
            _context.Users.Add(new User { Id = 102, Name = "Jane Doe", Email = "jane@example.com" });
            _context.SaveChanges();

            // create an instance of the controller to test
            _controller = new UserController(_context);
        }

        [TearDown]
        public void TearDown()
        {
            // dispose of the in-memory database after each test
            _context.Dispose();
        }

        [Test]
        public void Index_ReturnsViewWithListOfUsers()
        {
            // arrange

            // act
            var result = _controller.Index() as ViewResult;

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<System.Collections.Generic.List<User>>(result.Model);
            Assert.AreEqual(2, (result.Model as System.Collections.Generic.List<User>).Count);
        }

        [Test]
        public void Details_ReturnsViewWithUser()
        {
            // arrange
            int userId = 101;

            // act
            var result = _controller.Details(userId) as ViewResult;

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<User>(result.Model);
            Assert.AreEqual(userId, (result.Model as User).Id);
        }

        [Test]
        public void Create_ReturnsView()
        {
            // arrange

            // act
            var result = _controller.Create() as ViewResult;

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Create_ValidUser_RedirectsToIndex()
        {
            // arrange
            User user = new User { Name = "Test User", Email = "test@example.com" };

            // act
            var result = _controller.Create(user) as RedirectToActionResult;

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Index", result.ActionName);

            // check if the user was added to the database
            Assert.AreEqual(3, _context.Users.Count());
            Assert.IsTrue(_context.Users.Any(u => u.Name == "Test User" && u.Email == "test@example.com"));
        }

        [Test]
        public void Create_InvalidUser_ReturnsViewWithUser()
        {
            // arrange
            User user = new User { Name = "", Email = "" };
            _controller.ModelState.AddModelError("Name", "The Name field is required.");
            _controller.ModelState.AddModelError("Email", "The Email field is required.");

            // act
            var result = _controller.Create(user) as ViewResult;

            // assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<User>(result.Model);
            Assert.AreEqual(user, result.Model);
        }
    }
}