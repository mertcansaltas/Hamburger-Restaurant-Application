using Microsoft.AspNetCore.Mvc;

namespace Burger.UI.Controllers
{
    public class BlogController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SingleBlog()
        {
            return View();
        }
    }
}
