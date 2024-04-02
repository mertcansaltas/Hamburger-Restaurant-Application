using BurgerService.Services.BurgerService;
using BurgerService.Services.BurgerSiparisService;
using BurgerService.Services.MenuSiparisService;
using Microsoft.AspNetCore.Mvc;

namespace Burger.UI.Controllers
{
    public class HamburgerSiparisController : Controller
    {
        private readonly IBurgerSiparisService _burgersiparis;

        public HamburgerSiparisController(IBurgerSiparisService menusiparis)
        {
            _burgersiparis = menusiparis;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var icecek = await _burgersiparis.GetBurgerSiparis(id);
            if (icecek != null)
                await _burgersiparis.Delete(id);

            return RedirectToAction("Index", "Sepet");
        }
    }
}
