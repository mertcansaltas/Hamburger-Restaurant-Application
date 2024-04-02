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

namespace Burger.UI.Controllers
{
    public class SiparisController : Controller
    {
        private readonly ISepetService _sepetService;
        private readonly IIcecekSiparisService _icecekSiparisService;
        private readonly IEkstraMalzemeSiparisService _ekstraMalzemeSiparisService;
        private readonly ISosSiparisService _sosSiparisService;
        private readonly IMenuSiparisService _menuSiparisService;
        private readonly IBurgerSiparisService _hamburgerSiparisService;
        private readonly AppDbContext _context;
        private readonly SepetiTemizle _sepetiTemizle;

        public SiparisController(ISepetService sepetService, IIcecekSiparisService icecekSiparisService, IEkstraMalzemeSiparisService ekstraMalzemeSiparisService, IMenuSiparisService menuSiparisService, IBurgerSiparisService hamburgerSiparisService, AppDbContext context, SepetiTemizle sepetiTemizle, ISosSiparisService sosSiparisService)
        {
            _sepetService = sepetService;
            _icecekSiparisService = icecekSiparisService;
            _ekstraMalzemeSiparisService = ekstraMalzemeSiparisService;
            _menuSiparisService = menuSiparisService;
            _hamburgerSiparisService = hamburgerSiparisService;
            _context = context;
            _sepetiTemizle = sepetiTemizle;
            _sosSiparisService = sosSiparisService;
        }


        public async Task<IActionResult> Siparislerim()
        {
            var icecekSiparis = await _icecekSiparisService.GetAllIcecekSiparis();
            foreach (var item in icecekSiparis)
            {
                await _icecekSiparisService.DeletedStatus(item.ID);
            }

            var icecekSiparisListesi = await _icecekSiparisService.GetAllIcecekDeleted();




            var ekstraSiparis = await _ekstraMalzemeSiparisService.GetAllEkstraMalzemeSiparis();
            foreach (var item in ekstraSiparis)
            {
                await _ekstraMalzemeSiparisService.DeletedStatus(item.ID);
            }

            var ekstraSiparisListesi = await _ekstraMalzemeSiparisService.GetAllEkstraMalzemeDeleted();





            var hamburgerSiparis = await _hamburgerSiparisService.GetAllBurgerSiparis();
            foreach (var item in hamburgerSiparis)
            {
                await _hamburgerSiparisService.DeletedStatus(item.ID);
            }

            var hamburgerSiparisListesi = await _hamburgerSiparisService.GetAllBurgerSiparisDeleted();




            var menuSiparis = await _menuSiparisService.GetAllMenuSiparis();
            foreach (var item in menuSiparis)
            {
                await _menuSiparisService.DeletedStatus(item.ID);
            }

            var menuSiparisListesi = await _menuSiparisService.GetAllMenuSiparisDeleted();




            var sosSiparis = await _sosSiparisService.GetAllSosSiparis();
            foreach (var item in sosSiparis)
            {
                await _sosSiparisService.DeletedStatus(item.ID);
            }

            var sosSiparisListesi = await _sosSiparisService.GetAllSosSiparisDeleted();



            Siparis siparis = new Siparis()
            {              
                IcecekSiparisler= icecekSiparisListesi,
                SosSiparisler= sosSiparisListesi,
                EkstraMalzemeSiparisler=ekstraSiparisListesi,
                BurgerSiparisler=hamburgerSiparisListesi,
                MenuSiparisler=menuSiparisListesi
                
            };

            return View(siparis);
        }

      
    }
}
