using Microsoft.AspNetCore.Mvc;

namespace Burger.UI.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
