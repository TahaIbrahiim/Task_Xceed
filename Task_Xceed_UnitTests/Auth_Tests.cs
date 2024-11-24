using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Xceed.Controllers;

namespace Task_Xceed_UnitTests
{
    [TestFixture]
    public class Auth_Tests
    {
        private AuthController _controller;
        private AppDbContext _context;
        private ILogger<AuthController> _logger;

        [SetUp]
        public void SetUp()
        {
            var connectionString = "Server=.;Database=Task_Xceed;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True;";

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            _context = new AppDbContext(options);

    
            _logger = new LoggerFactory().CreateLogger<AuthController>();

            _controller = new AuthController(_context, _logger);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }


        [Test]
        public void Login_InvalidCredentials_ReturnsErrorMessage()
        {
            // Arrange
            var controller = _controller;

            // Act
            var result = controller.Login("Taha", "111") as ViewResult;

            // Assert
            Assert.That(result.ViewData["ErrorMessage"], Is.EqualTo("Invalid credentials!"));
        }

        [Test]
        public void Login_validCredentials_ReturnsErrorMessage()
        {
            // Arrange
            var controller = _controller;

            // Act
            var result = controller.Login("Admin", "12345600");

            // Assert
            Assert.That(result, Is.Not.Null);
        }


    }
}
