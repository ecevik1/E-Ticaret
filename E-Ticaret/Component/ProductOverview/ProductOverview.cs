using E_Ticaret.Data;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Component.ProductOverview
{
    public class ProductOverview : ViewComponent
    {
        private readonly ApplicationDbContext _context ;

        public ProductOverview(ApplicationDbContext context)
        {
            context = _context;
        }

        public IViewComponentResult Invoke()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
    }
}
