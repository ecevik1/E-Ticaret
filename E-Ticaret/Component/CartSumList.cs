using E_Ticaret.Data;
using E_Ticaret.Dto;
using E_Ticaret.Models;
using E_Ticaret.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Component
{
    public class CartSumList : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public CartSumList(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        // Asenkron olarak sayfasına veri gönderme işlemi
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CartItems> items = HttpContext.Session.GetJson<List<CartItems>>("Cart") ?? new List<CartItems>();
            CartViewModel cartvm = new CartViewModel
            {
                CartItems = items,
                TotalPrice = items.Sum(i => i.Quantity*i.Price)
            };
            return View(cartvm);

        }
    }
}
