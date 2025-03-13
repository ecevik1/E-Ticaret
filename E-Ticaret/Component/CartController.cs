using E_Ticaret.Data;
using E_Ticaret.Dto;
using E_Ticaret.Models;
using E_Ticaret.Session;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Component
{
    public class CartController : Controller
    {
        // Veritabanı bağlamını tutar
        private readonly ApplicationDbContext _context;

        // CartController yapıcı metodu, veritabanı bağlamını alır ve _context alanına atar
        public CartController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        // Sepet sayfasını görüntülemek için kullanılan eylem metodu
        public IActionResult Index()
        {
            // Oturumdan sepet verilerini alır, eğer sepet boşsa yeni bir liste oluşturur
            List<CartItems> items = HttpContext.Session.GetJson<List<CartItems>>("cart") ?? new List<CartItems>();

            // CartViewModel nesnesi oluşturur ve sepet öğelerini ve toplam fiyatı atar
            CartViewModel cartvm = new CartViewModel
            {
                CartItems = items,
                TotalPrice = items.Sum(i => i.Total)
            };

            // Sepet görünümünü döner ve CartViewModel'i görünümle birlikte gönderir
            return View(cartvm);
        }
    }
}

/*
 * Açıklamalar:
•	private readonly ApplicationDbContext _context;: Veritabanı bağlamını tutar.
•	public CartController(ApplicationDbContext dbContext): Yapıcı metot, veritabanı bağlamını alır ve _context alanına atar.
•	public IActionResult Index(): Sepet sayfasını görüntülemek için kullanılan eylem metodu.
•	List<CartItems> items = HttpContext.Session.GetJson<List<CartItems>>("cart") ?? new List<CartItems>();: Oturumdan sepet verilerini alır, eğer sepet boşsa yeni bir liste oluşturur.
•	CartViewModel cartvm = new CartViewModel: CartViewModel nesnesi oluşturur ve sepet öğelerini ve toplam fiyatı atar.
•	return View(cartvm);: Sepet görünümünü döner ve CartViewModel'i görünümle birlikte gönderir.
Bu metot, oturumda saklanan sepet verilerini alır, toplam fiyatı hesaplar ve sepet sayfasını görüntüler.
*/