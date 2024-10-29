using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Helpers;
using SalesAndPurchaseManagement.Models;
using System.Text.RegularExpressions;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SalesAndPurchaseManagement.Controllers
{
    public class ProductController : Controller
    {
        private SAPManagementContext db;

        private const int itemPerPage = 20;

        public ProductController(SAPManagementContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var furnitures = db.Products.Include(f => f.Manufacturer).AsQueryable();

            var paginatedProducts = furnitures.Take(itemPerPage).ToList();

            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = (int)Math.Ceiling(furnitures.Count() / (double)itemPerPage);
            ViewBag.SearchTerm = "";
            return View("Index", paginatedProducts);
        }

        public IActionResult Detail(int id)
        {
            var furniture= db.Products
                .Include(f => f.Category)
                .Include(f => f.Shape)
                .Include(f => f.Material)
                .Include(f => f.Country)
                .Include(f => f.Manufacturer)
                .Include(f => f.Feature)
                .Include(f => f.Characteristic)
                .Where(f => f.ProductId == id)
                .FirstOrDefault();
            if (furniture == null)
            {
                return NotFound();
            }
            return View("Detail", furniture);
        }

        public IActionResult Edit(int id)
        {
            var furniture = db.Products
                .Include(f => f.Category)
                .Include(f => f.Shape)
                .Include(f => f.Material)
                .Include(f => f.Country)
                .Include(f => f.Manufacturer)
                .Include(f => f.Feature)
                .Include(f => f.Characteristic)
                .Where(f => f.ProductId == id)
                .FirstOrDefault();
            if (furniture == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(db.Categories.ToList(), "CategoryId", "CategoryName");
            ViewBag.Shapes = new SelectList(db.Shapes, "ShapeId", "ShapeName");
            ViewBag.Materials = new SelectList(db.Materials, "MaterialId", "MaterialName");
            ViewBag.Countries = new SelectList(db.Countries, "CountryOfOriginId", "CountryName");
            ViewBag.Manufacturers = new SelectList(db.Manufacturers, "ManufacturerId", "ManufacturerName");
            ViewBag.Features = new SelectList(db.Features, "FeatureId", "FeatureName");
            ViewBag.Characteristics = new SelectList(db.Characteristics, "CharacteristicId", "CharacteristicName");
            return View("Edit", furniture);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,
             Product furniture,
            IFormFile? imageFile)
        {
            if (id != furniture.ProductId)
            {
                return NotFound();
            }

            var oldImage = furniture.Image;

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Kiểm tra định dạng tệp (nếu cần)
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            ModelState.AddModelError("Image", "Only image files are allowed.");
                            return View("Edit", furniture); // Trả về view với lỗi
                        }

                        // Tạo tên tệp duy nhất
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), AppDefaults.DefaultProductImageFolder, imageFile.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream).ConfigureAwait(false);
                        }
                        furniture.Image = imageFile.FileName;
                    }
                    else
                    {
                        furniture.Image = oldImage; // Giữ hình ảnh cũ nếu không có tệp mới
                    }

                    db.Update(furniture);
                    await db.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Product updated successfully."; // Thông báo thành công
                    return RedirectToAction(nameof(Detail), new { id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!db.Products.Any(f => f.ProductId == id))
                    {
                        return NotFound();
                    }
                    throw; // Xem xét ghi lại ngoại lệ này
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the product. Please try again.");
                    // Ghi lại ngoại lệ
                }
            }

            // Repopulate ViewBag with data in case of invalid model
            furniture.Image = oldImage; // Giữ hình ảnh cũ nếu model không hợp lệ
            ViewBag.Categories = new SelectList(await db.Categories.ToListAsync(), "CategoryId", "CategoryName");
            ViewBag.Shapes = new SelectList(await db.Shapes.ToListAsync(), "ShapeId", "ShapeName");
            ViewBag.Materials = new SelectList(await db.Materials.ToListAsync(), "MaterialId", "MaterialName");
            ViewBag.Countries = new SelectList(await db.Countries.ToListAsync(), "CountryOfOriginId", "CountryName");
            ViewBag.Manufacturers = new SelectList(await db.Manufacturers.ToListAsync(), "ManufacturerId", "ManufacturerName");
            ViewBag.Features = new SelectList(await db.Features.ToListAsync(), "FeatureId", "FeatureName");
            ViewBag.Characteristics = new SelectList(await db.Characteristics.ToListAsync(), "CharacteristicId", "CharacteristicName");

            return View("Edit", furniture);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await db.Categories.ToListAsync(), "CategoryId", "CategoryName");
            ViewBag.Shapes = new SelectList(await db.Shapes.ToListAsync(), "ShapeId", "ShapeName");
            ViewBag.Materials = new SelectList(await db.Materials.ToListAsync(), "MaterialId", "MaterialName");
            ViewBag.Countries = new SelectList(await db.Countries.ToListAsync(), "CountryOfOriginId", "CountryName");
            ViewBag.Manufacturers = new SelectList(await db.Manufacturers.ToListAsync(), "ManufacturerId", "ManufacturerName");
            ViewBag.Features = new SelectList(await db.Features.ToListAsync(), "FeatureId", "FeatureName");
            ViewBag.Characteristics = new SelectList(await db.Characteristics.ToListAsync(), "CharacteristicId", "CharacteristicName");
            return View("Create");
        }


        [HttpPost]
        public async Task<IActionResult> Create(
            [Bind("Image, ProductName, Length, Width, Height, Color, CategoryId, ShapeId, MaterialId, CountryOfOriginId, " +
            "ManufacturerId, FeatureId, CharacteristicId, Quantity, PurchasePrice, SellingPrice, WarrantyPeriod, Notes")] Product furniture, 
            IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Kiểm tra định dạng tệp (nếu cần)
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                        var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

                        if (!allowedExtensions.Contains(fileExtension))
                        {
                            ModelState.AddModelError("Image", "Only image files are allowed.");
                            return View("Create", furniture); // Trả về view với lỗi
                        }

                        // Tạo tên tệp duy nhất
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), AppDefaults.DefaultProductImageFolder, imageFile.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        furniture.Image = imageFile.FileName;
                    }

                    db.Add(furniture);
                    await db.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Product created successfully."; // Thông báo thành công
                    return RedirectToAction(nameof(Detail), new { id = furniture.ProductId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the product. Please try again.");
                    // Ghi lại ngoại lệ
                }
            }

            foreach (var state in ModelState)
            {
                var key = state.Key; // Tên của trường
                var errors = state.Value.Errors; // Các lỗi liên quan đến trường đó

                foreach (var error in errors)
                {
                    Console.WriteLine($"Field: {key}, Error: {error.ErrorMessage}");
                }
            }

            // Repopulate ViewBag with data in case of invalid model
            ViewBag.Categories = new SelectList(await db.Categories.ToListAsync(), "CategoryId", "CategoryName");
            ViewBag.Shapes = new SelectList(await db.Shapes.ToListAsync(), "ShapeId", "ShapeName");
            ViewBag.Materials = new SelectList(await db.Materials.ToListAsync(), "MaterialId", "MaterialName");
            ViewBag.Countries = new SelectList(await db.Countries.ToListAsync(), "CountryOfOriginId", "CountryName");
            ViewBag.Manufacturers = new SelectList(await db.Manufacturers.ToListAsync(), "ManufacturerId", "ManufacturerName");
            ViewBag.Features = new SelectList(await db.Features.ToListAsync(), "FeatureId", "FeatureName");
            ViewBag.Characteristics = new SelectList(await db.Characteristics.ToListAsync(), "CharacteristicId", "CharacteristicName");

            return View("Create", furniture);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var furniture = await db.Products.FindAsync(id);
            if (furniture == null)
            {
                return NotFound();
            }

            db.Products.Remove(furniture);
            await db.SaveChangesAsync();
            TempData["SuccessMessage"] = "Product deleted successfully."; // Thông báo thành công
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetProducts(int page = 1, string searchTerm = "")
        {
            if (searchTerm == null)
            {
                searchTerm = "";
            }

            var keywords = Regex.Split(searchTerm, @"\s+").Where(k => !string.IsNullOrWhiteSpace(k)).Select(k => k.ToLower()).ToList();

            var products = db.Products
                .Include(f => f.Category)
                .Include(f => f.Shape)
                .Include(f => f.Material)
                .Include(f => f.Country)
                .Include(f => f.Manufacturer)
                .Include(f => f.Feature)
                .Include(f => f.Characteristic)
                .AsEnumerable(); // Fetch data into memory

            IEnumerable<Product> tmp;

            foreach (var keyword in keywords)
            {
                tmp = products.Where(f =>
                    f.ProductName.ToLower().Contains(keyword) ||
                    f.ProductId.ToString().Contains(keyword) ||
                    f.GetSize().ToLower().Contains(keyword) ||
                    f.Color.ToLower().Contains(keyword) ||
                    (f.Category != null && f.Category.CategoryName.ToLower().Contains(keyword)) ||
                    (f.Shape != null && f.Shape.ShapeName.ToLower().Contains(keyword)) ||
                    (f.Material != null && f.Material.MaterialName.ToLower().Contains(keyword)) ||
                    (f.Country != null && f.Country.CountryName.ToLower().Contains(keyword)) ||
                    (f.Manufacturer != null && f.Manufacturer.ManufacturerName.ToLower().Contains(keyword)) ||
                    (f.Feature != null && f.Feature.FeatureName.ToLower().Contains(keyword)) ||
                    (f.Characteristic != null && f.Characteristic.CharacteristicName.ToLower().Contains(keyword)) ||
                    f.PurchasePrice.ToString().Contains(keyword) ||
                    f.SellingPrice.ToString().Contains(keyword)
                );
                if (tmp.Count() > 0)
                {
                    products = tmp;
                }
            }

            var paginatedProducts = products
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(products.Count() / (double)itemPerPage);
            ViewBag.SearchTerm = searchTerm;

            return PartialView("_ProductListPartial", paginatedProducts);
        }
    }
}
