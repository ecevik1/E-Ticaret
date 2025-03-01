using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    public class HomeDeneme : Controller
    {
        // GET: HomeDeneme
        public IActionResult Index()
        {
            return View();
        }
    }
}
