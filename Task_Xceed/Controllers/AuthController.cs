using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF;
using System.Diagnostics;
using Task_Xceed.Models;

namespace Task_Xceed.Controllers
{
    public class AuthController : Controller
    {
        #region PrivateReadOnly
        private readonly AppDbContext _dbcontext;
        private readonly ILogger<AuthController> _logger;
        #endregion
        public AuthController(AppDbContext dbcontext, ILogger<AuthController> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public IActionResult Login()
        {
            var user = new User();
            return View(user);
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {
                var user = _dbcontext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
                if (user != null)
                {
                    HttpContext.Session.SetString("UserRole", user.Role);
                    HttpContext.Session.SetString("UserName", user.Username);
                    HttpContext.Session.SetInt32("UserID", user.Id);
                    _logger.LogInformation($"User {username} logged in successfully.");
                    return RedirectToAction("Index", "Product");
                }

                _logger.LogWarning($"Failed login attempt for user {username}.");
                ViewBag.ErrorMessage = "Invalid credentials!";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the login.");
                ViewBag.ErrorMessage = "An error occurred. Please try again later.";
                return View();
            }
        }

        public IActionResult Logout()
        {
            _logger.LogInformation("User logged out.");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var user = new User();
            return View(user);
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbcontext.Users.Add(user);
                    _dbcontext.SaveChanges();
                    _logger.LogInformation($"New user registered: {user.Username}");
                    return RedirectToAction("Login");
                }
                _logger.LogWarning("Registration failed due to invalid model state.");
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the registration.");
                ViewBag.ErrorMessage = "An error occurred. Please try again later.";
                return View(user);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("Error occurred: " + Activity.Current?.Id ?? HttpContext.TraceIdentifier);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
