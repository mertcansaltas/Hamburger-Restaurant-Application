using Burger.UI.HelperClass;
using Burger.UI.Models.VMs;
using BurgerService.Services.BurgerSiparisService;
using BurgerService.Services.EkstraMalzemeSiparisService;
using BurgerService.Services.IcecekSiparisService;
using BurgerService.Services.MenuSiparisService;
using BurgerService.Services.SepetService;
using BurgerService.Services.SosSiparisService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;
using MVCPROJE.ENTITIES.Enum_s;

namespace Burger.UI.Controllers
{
    public class SepetController : Controller
    {
        private readonly ISepetService _sepetService;
        private readonly IIcecekSiparisService _icecekSiparisService;
        private readonly IEkstraMalzemeSiparisService _ekstraMalzemeSiparisService;
        private readonly ISosSiparisService _sosSiparisService;
        private readonly IMenuSiparisService _menuSiparisService;
        private readonly IBurgerSiparisService _hamburgerSiparisService;
        private readonly SepetiTemizle _sepetiTemizle;
        private readonly AppDbContext _context;

        public SepetController(ISepetService sepetService, IIcecekSiparisService icecekSiparisService, IEkstraMalzemeSiparisService ekstraMalzemeSiparisService, IMenuSiparisService menuSiparisService, IBurgerSiparisService hamburgerSiparisService, SepetiTemizle sepetiTemizle, AppDbContext context, ISosSiparisService sosSiparisService)
        {
            _sepetService = sepetService;
            _icecekSiparisService = icecekSiparisService;
            _ekstraMalzemeSiparisService = ekstraMalzemeSiparisService;
            _menuSiparisService = menuSiparisService;
            _hamburgerSiparisService = hamburgerSiparisService;
            _sepetiTemizle = sepetiTemizle;
            _context = context;
            _sosSiparisService = sosSiparisService;
        }



        public async Task<IActionResult> Index()
        {
            var icecekSiparisListesi= await _icecekSiparisService.GetAllIcecekSiparisActive();
            var hamburgerSiparisListesi= await _hamburgerSiparisService.GetAllBurgerSiparisActive();
            var menuSiparisListesi= await _menuSiparisService.GetAllMenuSiparisActive();
            var sosSiparisListesi = await _sosSiparisService.GetAllSosSiparisActive();
            var ekstraMalzemeSiparisListesi= await _ekstraMalzemeSiparisService.GetAllEkstraMalzemeSiparisActive();     
            var sepetler = await _sepetService.GetAllSepet();

            SepetVM sepetVM = new SepetVM()
            {
                Sepetler = sepetler,
                IcecekSiparisler = icecekSiparisListesi,
                BurgerSiparisler = hamburgerSiparisListesi,
                MenuSiparisler= menuSiparisListesi, 
                EkstraMalzemeSiparisler=ekstraMalzemeSiparisListesi,
                              
            };
            return View(sepetVM);
        }

        public async Task<IActionResult> SepetiTemizle()
        {
            await _sepetiTemizle.Temizle(HttpContext);
            return RedirectToAction("Index","Sepet");
        }

       
    }
}
