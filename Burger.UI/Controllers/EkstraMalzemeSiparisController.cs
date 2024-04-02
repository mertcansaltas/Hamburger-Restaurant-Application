using BurgerService.Services.BurgerSiparisService;
using BurgerService.Services.EkstraMalzemeSiparisService;
using Microsoft.AspNetCore.Mvc;

namespace Burger.UI.Controllers
{
    public class EkstraMalzemeSiparisController : Controller
    {

        private readonly IEkstraMalzemeSiparisService _ekstrasiparis;

        public EkstraMalzemeSiparisController(IEkstraMalzemeSiparisService ekstrasiparis)
        {
            _ekstrasiparis = ekstrasiparis;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var icecek = await _ekstrasiparis.GetEkstraMalzemeSiparis(id);
            if (icecek != null)
                await _ekstrasiparis.Delete(id);

            return RedirectToAction("Index", "Sepet");
        }
    }
}
