using E_Ticaret.Data;
using E_Ticaret.Dto;
using E_Ticaret.Models;
using E_Ticaret.Session;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Component
{
    public class LayoutCartIconViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public LayoutCartIconViewComponent(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CartItems> items = HttpContext.Session.GetJson<List<CartItems>>("Cart") ?? new List<CartItems>();
            int totalItemCount = items.Sum(item => item.Quantity);
            CartViewModel cartvm = new CartViewModel
            {
                CartItems = items,
                TotalPrice = items.Sum(item => item.Total) // TotalPrice hesaplanmalı
            };
            // TotalItemCount değerini yansıtmak için yeni bir CartViewModel türevi oluşturun
            var cartvmWithTotalItemCount = new CartViewModelWithTotalItemCount(cartvm, totalItemCount);
            return View(cartvmWithTotalItemCount);
        }

        // Yeni bir türev sınıf oluşturun
        public class CartViewModelWithTotalItemCount : CartViewModel
        {
            public CartViewModelWithTotalItemCount(CartViewModel baseModel, int totalItemCount)
            {
                CartItems = baseModel.CartItems;
                TotalPrice = baseModel.TotalPrice;
                TotalItemCount = totalItemCount;
            }

            public new int TotalItemCount { get; set; }
        }

    }
}
