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
            List<CartItems> items = HttpContext.Session.GetJson<List<CartItems>>("Cart") ?? new List<CartItems>();

            // CartViewModel nesnesi oluşturur ve sepet öğelerini ve toplam fiyatı atar
            CartViewModel cartvm = new CartViewModel
            {
                CartItems = items,
                TotalPrice = items.Sum(i => i.Total)
            };

            // Sepet görünümünü döner ve CartViewModel'i görünümle birlikte gönderir
            return View(cartvm);
        }

        // Sepete ürün eklemek için kullanılan eylem metodu
        public async Task<IActionResult> Add(int id)
        {
            // Ürünü veritabanından alır
            Products product = await _context.Products.FindAsync(id);
            // Ürün varsa
            if (product != null)
            {
                // Oturumdan sepet verilerini alır, eğer sepet boşsa yeni bir liste oluşturur
                List<CartItems> items = HttpContext.Session.GetJson<List<CartItems>>("Cart") ?? new List<CartItems>();
                // Sepette aynı ürün varsa miktarını artırır
                CartItems cartItem = items.FirstOrDefault(i => i.ProductId == id);
                if (cartItem == null)
                {
                    items.Add(new CartItems(product));
                }
                else
                {
                    cartItem.Quantity++;
                }
                // Sepet verilerini oturuma yazar
                HttpContext.Session.SetJson("Cart", items);
                TempData["message"] = $"{product.ProductName} Sepete Eklenmiştir";
            }
            // Sepet sayfasına yönlendirir
            string referer = Request.Headers["Referer"].ToString();
            if (string.IsNullOrEmpty(referer))
            {
                return RedirectToAction("Index");
            }
            return Redirect(referer);
        }

        // Sepetten ürün miktarını azaltmak için kullanılan eylem metodu
        public async Task<IActionResult> Decrease(int id)
        {
            // Ürünü veritabanından alır
            Products product = await _context.Products.FindAsync(id);
            // Ürün varsa
            if (product != null)
            {
                // Oturumdan sepet verilerini alır, eğer sepet boşsa yeni bir liste oluşturur
                List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart") ?? new List<CartItems>();
                // Sepette aynı ürün varsa miktarını azaltır
                CartItems cartItem = cart.FirstOrDefault(c => c.ProductId == id);
                if (cartItem != null)
                {
                    if (cartItem.Quantity > 1)
                    {
                        cartItem.Quantity--;
                    }
                    else
                    {
                        cart.Remove(cartItem);
                    }
                }
                // Sepet verilerini oturuma yazar
                HttpContext.Session.SetJson("Cart", cart);
                TempData["message"] = $"{product.ProductName} Sepetten Çıkarılmıştır";
            }

            // Sepet sayfasına yönlendirir
            return RedirectToAction("Index");
        }

        // Sepetten ürün kaldırmak için kullanılan eylem metodu
        [HttpPost]
        public IActionResult Remove(int id)
        {
            List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart");
            cart.RemoveAll(c => c.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            TempData["message"] = "Ürün Sepetten Silinmiştir";
            return RedirectToAction("Index");
        }

        // Sepeti güncellemek için kullanılan eylem metodu
        [HttpPost]
        public IActionResult UpdateCart(Dictionary<int, int> quantities)
        {
            List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart") ?? new List<CartItems>();

            foreach (var item in cart)
            {
                if (quantities.TryGetValue(item.ProductId, out int quantity))
                {
                    item.Quantity = quantity;
                }
            }

            HttpContext.Session.SetJson("Cart", cart);
            TempData["message"] = "Sepet Güncellenmiştir";
            return RedirectToAction("Index");
        }

        // Ürün miktarını güncellemek için kullanılan eylem metodu
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            List<CartItems> cart = HttpContext.Session.GetJson<List<CartItems>>("Cart") ?? new List<CartItems>();

            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                HttpContext.Session.SetJson("Cart", cart);
                TempData["message"] = "Ürün Miktarı Güncellenmiştir";
            }

            return RedirectToAction("Index");
        }

        // Sepeti temizlemek için kullanılan eylem metodu
        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            TempData["message"] = "Sepet Temizlenmiştir";
            return RedirectToAction("Index");
        }
    }
}
