using Burger.UI.Models.VMs;
using BurgerService.DTOs;
using BurgerService.Services.MenuService;
using BurgerService.Services.SosService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SosController : Controller
    {
        private readonly ISosService _sosService;
        private readonly AppDbContext db;

        public SosController(ISosService sosService, AppDbContext db)
        {
            _sosService = sosService;
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var sos = await _sosService.GetAllSos();
            return View(sos);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sos = await _sosService.GetSos(id);
            if (sos != null)
                await _sosService.Delete(id);
            return RedirectToAction("Index", "Sos");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SosCreateDTO sosDto)
        {
            if (ModelState.IsValid)
            {

                // Ekstramalzeme veritabanına kaydetme işlemi
                Sos sos = new Sos()
                {
                    Adi = sosDto.Adi,
                    Fiyat = sosDto.Fiyat,
                    Resim = sosDto.ImageFile,
                    Aciklama = sosDto.Aciklama,
                    // Diğer alanlarınız varsa burada atayabilirsiniz
                };
                _sosService.Create(sosDto);
                // Başarılı olursa, kullanıcıyı başka bir sayfaya yönlendirin (örneğin, Index sayfasına).
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // ModelState geçerli değilse, aynı görünümü tekrar gösterin, böylece kullanıcı hataları görebilir.
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            SosCreateDTO c = db.Soslar.Where(x => x.ID.Equals(id)).Select(x => new SosCreateDTO
            {
                Adi = x.Adi,
                Fiyat = x.Fiyat,
                ImageFile = x.Resim,
                Aciklama = x.Aciklama
            }).FirstOrDefault();
            return View(c);

        }
        [HttpPost]
        public IActionResult Edit(int id, SosCreateDTO dt)
        {
            Sos sos = db.Soslar.FirstOrDefault(x => x.ID.Equals(id));
            sos.Adi = dt.Adi;
            sos.Fiyat = dt.Fiyat;
            sos.Aciklama = dt.Aciklama;
            sos.Resim = dt.ImageFile;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}