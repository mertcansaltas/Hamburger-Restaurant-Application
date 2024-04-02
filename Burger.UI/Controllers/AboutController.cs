using Microsoft.AspNetCore.Mvc;

namespace Burger.UI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
