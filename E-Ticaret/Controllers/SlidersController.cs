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
    public class SlidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SlidersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sliders
        public async Task<IActionResult> Index()
        {
            // _context.Slider null olup olmadığını kontrol ediyoruz.
            if (_context.Slider == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Slider' is null."); // null olması durumunda hata mesajı döndürür.
            }
            return View(await _context.Slider.ToListAsync());
        }

        // GET: Sliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // id veya _context.Slider null olup olmadığını kontrol ediyoruz.
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sliders/Create
        // Overposting saldırılarına karşı koruma sağlamak için, bağlamak istediğiniz belirli özellikleri etkinleştirin.
        // Daha fazla bilgi için http://go.microsoft.com/fwlink/?LinkId=317598 adresine bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SliderId,SliderName,Header1,Header2,SliderContext,SliderDescription,SliderImage")] Slider slider, IFormFile ImageUpload)
        {
            if (ImageUpload != null)
            {
                string uzanti = Path.GetExtension(ImageUpload.FileName);
                string yeniisim = Guid.NewGuid().ToString() + uzanti; // Benzersiz bir isim oluşturur.
                string klasorYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Sliders"); // Klasör yolu oluşturulur.
                string dosyaYolu = Path.Combine(klasorYolu, yeniisim); // Dosya yolu oluşturulur.

                if (!Directory.Exists(klasorYolu))
                {
                    Directory.CreateDirectory(klasorYolu);
                }

                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    await ImageUpload.CopyToAsync(stream);
                }

                slider.SliderImage = yeniisim; // Dosya adı atanır.
            }
            else
            {
                slider.SliderImage = "?"; // Görsel yüklenmediğinde varsayılan değer.
            }

            if (ModelState.IsValid)
            {
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        // GET: Sliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // id veya _context.Slider null olup olmadığını kontrol ediyoruz.
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: Sliders/Edit/5
        // Overposting saldırılarına karşı koruma sağlamak için, bağlamak istediğiniz belirli özellikleri etkinleştirin.
        // Daha fazla bilgi için http://go.microsoft.com/fwlink/?LinkId=317598 adresine bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SliderId,SliderName,Header1,Header2,SliderContext,SliderDescription,SliderImage")] Slider slider, IFormFile ImageUpload)
        {
            if (id != slider.SliderId)
            {
                return NotFound();
            }

            if (ImageUpload != null)
            {
                string uzanti = Path.GetExtension(ImageUpload.FileName);
                string yeniisim = Guid.NewGuid().ToString() + uzanti; // Benzersiz bir isim oluşturur.
                string klasorYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Sliders"); // Klasör yolu oluşturulur.
                string dosyaYolu = Path.Combine(klasorYolu, yeniisim); // Dosya yolu oluşturulur.
                if (!Directory.Exists(klasorYolu))
                {
                    Directory.CreateDirectory(klasorYolu);
                }
                using (var stream = new FileStream(dosyaYolu, FileMode.Create))
                {
                    await ImageUpload.CopyToAsync(stream);
                }
                slider.SliderImage = yeniisim; // Dosya adı atanır.
            }
            else
            {
                slider.SliderImage = "?"; // Görsel yüklenmediğinde varsayılan değer.
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.SliderId))
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
            return View(slider);
        }

        // GET: Sliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // id veya _context.Slider null olup olmadığını kontrol ediyoruz.
            if (id == null || _context.Slider == null)
            {
                return NotFound();
            }

            var slider = await _context.Slider
                .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // _context.Slider null olup olmadığını kontrol ediyoruz.
            if (_context.Slider == null)
            {
                return NotFound();
            }
            var slider = await _context.Slider.FindAsync(id);
            if (slider != null)
            {
                // Dosya silme
                if (!string.IsNullOrEmpty(slider.SliderImage))
                {
                    string yol = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Sliders", slider.SliderImage);
                    FileInfo yolFile = new FileInfo(yol);
                    if (yolFile.Exists)
                    {
                        System.IO.File.Delete(yolFile.FullName);
                        yolFile.Delete();
                    }
                }
                // Dosya silme
                _context.Slider.Remove(slider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
            // _context.Slider null olup olmadığını kontrol ediyoruz.
            if (_context.Slider == null)
            {
                return false;
            }
            return _context.Slider.Any(e => e.SliderId == id);
        }
    }
}
