using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Ticaret.Data;
using E_Ticaret.Models;

namespace E_Ticaret.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.kateliste = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductDescription,ProductPrice,ProductPicture,CategoryId,ProductCode")] Products products, IFormFile ImageUpload)
        {
            if (ImageUpload != null)
            {
                string uzanti = Path.GetExtension(ImageUpload.FileName);
                string yeniisim = Guid.NewGuid().ToString() + uzanti; // Benzersiz bir isim oluşturur.
                string klasorYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Urunler"); // Klasör yolu oluşturulur.
                string dosyaYolu = Path.Combine(klasorYolu, yeniisim); // Dosya yolu oluşturulur.

                if (!Directory.Exists(klasorYolu))
                {
                    Directory.CreateDirectory(klasorYolu);
                }

                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    await ImageUpload.CopyToAsync(stream);
                }

                products.ProductPicture = yeniisim; // Dosya adı atanır.
            }
            else
            {
                products.ProductPicture = "?"; // Görsel yüklenmediğinde varsayılan değer.
            }

            // CategoryId'ye göre Category ile bağlama işlemi
            if (products.CategoryId != 0)
            {
                var category = await _context.Categories.FindAsync(products.CategoryId);
                if (category != null)
                {
                    products.Category = category;
                }
                else
                {
                    ModelState.AddModelError("CategoryId", "Invalid CategoryId");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.kateliste = new SelectList(_context.Categories, "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            ViewBag.kateliste = new SelectList(_context.Categories, "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductDescription,ProductPrice,ProductPicture,CategoryId,ProductCode")] Products products, IFormFile ImageUpload)
        {
            if (id != products.ProductId)
            {
                return NotFound();
            }

            if (ImageUpload != null)
            {
                string uzanti = Path.GetExtension(ImageUpload.FileName);
                string yeniisim = Guid.NewGuid().ToString() + uzanti; // Benzersiz bir isim oluşturur.
                string klasorYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Urunler"); // Klasör yolu oluşturulur.
                string dosyaYolu = Path.Combine(klasorYolu, yeniisim); // Dosya yolu oluşturulur.

                if (!Directory.Exists(klasorYolu))
                {
                    Directory.CreateDirectory(klasorYolu);
                }

                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    await ImageUpload.CopyToAsync(stream);
                }

                products.ProductPicture = yeniisim; // Dosya adı atanır.
            }

            // CategoryId'ye göre Category ile bağlama işlemi
            if (products.CategoryId != 0)
            {
                var category = await _context.Categories.FindAsync(products.CategoryId);
                if (category != null)
                {
                    products.Category = category;
                }
                else
                {
                    ModelState.AddModelError("CategoryId", "Invalid CategoryId");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.kateliste = new SelectList(_context.Categories, "CategoryId", "CategoryName", products.CategoryId);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products' is null.");
            }

            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);

                // Dosya silme
                if (!string.IsNullOrEmpty(products.ProductPicture))
                {
                    string yol = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Urunler", products.ProductPicture);
                    FileInfo yolFile = new FileInfo(yol);
                    if (yolFile.Exists)
                    {
                        System.IO.File.Delete(yolFile.FullName);
                        yolFile.Delete();
                    }
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}


