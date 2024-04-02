using BurgerService.Services.IcecekSiparisService;
using Microsoft.AspNetCore.Mvc;
using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.Controllers
{
    public class IcecekSiparislerController : Controller
    {
        private readonly IIcecekSiparisService _iceceksiparis;

        public IcecekSiparislerController(IIcecekSiparisService iceceksiparis)
        {
            _iceceksiparis = iceceksiparis;
        }

        public async Task<IActionResult> Delete(int id)
        {
            var icecek = await _iceceksiparis.GetIcecekSiparis(id);
            if (icecek != null)
                await _iceceksiparis.Delete(id);
            
            return RedirectToAction("Index","Sepet");
        }
    }
}
