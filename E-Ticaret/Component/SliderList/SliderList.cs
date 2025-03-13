using E_Ticaret.Data;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Component.SliderList
{
    public class SliderList:ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SliderList(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public IViewComponentResult Invoke()//SliderList.cshtml sayfasına veri gönderme işlemi
        {
            var result = _context.Slider.ToList();
            return View(result);
        }
    }
}
