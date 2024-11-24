using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Xceed.Controllers
{
    public class ProductController : Controller
    {
        #region PrivateReadOnly
        private readonly IUnitOfWork _uintOfWork;
        private readonly ILogger<ProductController> _logger;
        #endregion
        public ProductController(IUnitOfWork uintOfWork, ILogger<ProductController> logger)
        {
            _uintOfWork = uintOfWork;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var role = HttpContext.Session.GetString("UserRole");

                ViewBag.category = (await _uintOfWork.CategoryRepository.GetAll())
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name
                    });

                if (role == "Admin")
                {
                    var products = await _uintOfWork.ProductRepository.GetAll();
                    return View("Index", products);
                }
                else
                {
                    var products = await _uintOfWork.ProductRepository.GetAll();
                    return View("UserIndex", products);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the products.");
                return View();
            }
        }

        public async Task<IActionResult> UserIndex()
        {
            try
            {
                var products = await _uintOfWork.ProductRepository.GetAll();
                return View(products);
            }
            catch (Exception )
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the product list.");
                return View();
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var Category = await _uintOfWork.CategoryRepository.GetAll();
                var product = await _uintOfWork.ProductRepository.GetById(id);

                var user = await _uintOfWork.UserRepository.GetById(product.CreatedByUserId);

                if (user != null)
                {
                    ViewBag.CreatedByUserName = user.Username;
                }

                if (product is null)
                {
                    return NotFound();
                }

                return View(product);
            }
            catch (Exception )
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the product details.");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var role = HttpContext.Session.GetString("UserRole");

                if (role != "Admin")
                    return Unauthorized();

                ViewBag.category = (await _uintOfWork.CategoryRepository.GetAll())
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name
                    });

                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the create product form.");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile image)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserID");
                product.CreationDate = DateTime.Now;
                product.CreatedByUserId = userId.Value;

                int MaxImageSize = 1 * 1024 * 1024; // 1 MB in bytes

                if (image != null && image.Length > 0)
                {
                    if (image.Length > MaxImageSize)
                    {
                        ModelState.AddModelError("Image", "Image size should not exceed 1 MB.");
                        return View(product);
                    }

                    using (var ms = new MemoryStream())
                    {
                        await image.CopyToAsync(ms);
                        product.Image = ms.ToArray();
                    }
                }

                await _uintOfWork.ProductRepository.Add(product);
                await _uintOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the product.");
                return View(product);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var role = HttpContext.Session.GetString("UserRole");

                if (role != "Admin")
                    return Unauthorized();

                var product = await _uintOfWork.ProductRepository.GetById(id);

                // Old Image Load
                if (product.Image != null)
                {
                    var base64Image = Convert.ToBase64String(product.Image);
                    ViewData["ImageBase64"] = base64Image;
                }

                if (product is null)
                    return NotFound();

                ViewBag.category = (await _uintOfWork.CategoryRepository.GetAll())
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name
                    });

                return View(product);
            }
            catch (Exception )
            {
                ModelState.AddModelError(string.Empty, "An error occurred while loading the edit product form.");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile image)
        {
            ViewBag.category = (await _uintOfWork.CategoryRepository.GetAll())
                   .Select(a => new SelectListItem
                   {
                       Value = a.Id.ToString(),
                       Text = a.Name
                   });

            try
            {
                var role = HttpContext.Session.GetString("UserRole");

                if (role != "Admin")
                    return Unauthorized();

                if (id != product.Id)
                    return BadRequest();

                int MaxImageSize = 1 * 1024 * 1024; // 1 MB in bytes

                if (image != null && image.Length > 0)
                {
                    if (image.Length > MaxImageSize)
                    {
                        TempData["Error"] = "Image size should not exceed 1 MB.";
                        return View(product);
                    }

                    using (var ms = new MemoryStream())
                    {
                        await image.CopyToAsync(ms);
                        product.Image = ms.ToArray();
                    }
                }
                else
                {
                    // Old Image
                    var existingProduct = await _uintOfWork.ProductRepository.GetById(id);
                    product.Image = existingProduct.Image; 
                }

                product.LastUpdatedDateTime = DateTime.Now;
                var userId = HttpContext.Session.GetInt32("UserID");
                product.LastUpdatedByUserId = userId.Value;

                _uintOfWork.ProductRepository.Edit(product);
                await _uintOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception )
            {
                TempData["Error"] = "An error occurred while updating the product. Please try again.";
                return View(product);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var role = HttpContext.Session.GetString("UserRole");

                if (role != "Admin")
                    return Unauthorized();

                var product = await _uintOfWork.ProductRepository.GetById(id);

                if (product is null)
                    return NotFound();

                _uintOfWork.ProductRepository.Delete(product);
                await _uintOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the product.");
                return View();
            }
        }

        public async Task<IActionResult> Filter(int? id)
        {
            try
            {
                ViewBag.category = (await _uintOfWork.CategoryRepository.GetAll())
                    .Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name
                    });

                ViewBag.categoryID = id;

                IEnumerable<Product> filteredProducts;

                if (id == null)
                {
                    filteredProducts = await _uintOfWork.ProductRepository.GetAll();
                }
                else
                {
                    filteredProducts = (await _uintOfWork.ProductRepository.GetAll())
                        .Where(p => p.Category.Id == id);
                }

                return View("Filter", filteredProducts);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while filtering the products.");
                return View();
            }
        }

    }
}
