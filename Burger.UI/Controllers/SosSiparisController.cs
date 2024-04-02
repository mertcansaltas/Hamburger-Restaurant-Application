using BurgerService.Services.IcecekSiparisService;
using BurgerService.Services.SosService;
using BurgerService.Services.SosSiparisService;
using Microsoft.AspNetCore.Mvc;

namespace Burger.UI.Controllers
{
    public class SosSiparisController : Controller
    {
        private readonly ISosSiparisService _sossiparis;

        public SosSiparisController(ISosSiparisService sossiparis)
        {
            _sossiparis = sossiparis;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var icecek = await _sossiparis.GetSosSiparis(id);
            if (icecek != null)
                await _sossiparis.Delete(id);

            return RedirectToAction("Index", "Sepet");
        }
    }
}
