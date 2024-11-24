using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RepositoryPatternWithUOW.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Xceed.Controllers;

namespace Task_Xceed.UnitTesting
{
    [TestFixture]
    public class Auth_Test
    {
        private AuthController _controller;
        private DbContextOptions<AppDbContext> _options;


        [Test]
        public void Login_InvalidCredentials_ReturnsErrorMessage()
        {
            // Arrange
            var controller = new AuthController();
            string invalidUsername = "wrongUser";
            string invalidPassword = "wrongPassword";

            // Act
            var result = controller.Login(invalidUsername, invalidPassword) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null); // Ensure a ViewResult is returned
            Assert.That(controller.ViewBag.ErrorMessage, Is.EqualTo("Invalid credentials!"));
        }

    }
}
