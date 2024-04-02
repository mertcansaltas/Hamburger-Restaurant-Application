using BurgerService.Services.MenuSiparisService;
using BurgerService.Services.SosSiparisService;
using Microsoft.AspNetCore.Mvc;

namespace Burger.UI.Controllers
{
    public class MenulerSiparisController : Controller
    {
        private readonly IMenuSiparisService _menusiparis;

        public MenulerSiparisController(IMenuSiparisService menusiparis)
        {
            _menusiparis = menusiparis;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var icecek = await _menusiparis.GetMenuSiparis(id);
            if (icecek != null)
                await _menusiparis.Delete(id);

            return RedirectToAction("Index", "Sepet");
        }
    }
}
