using BurgerService.DTOs;
using BurgerService.Services.BurgerService;
using BurgerService.Services.MenuService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HamburgerController : Controller
    {
        private readonly IHamburgerService _HamburgerService;
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public HamburgerController(IHamburgerService hamburgerService, AppDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _HamburgerService = hamburgerService;
            this.db = db;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var menuler = await _HamburgerService.GetAllBurger();
            return View(menuler);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var Icecek = await _HamburgerService.GetBurger(id);
            if (Icecek != null)
                await _HamburgerService.Delete(id);
            return RedirectToAction("Index", "Hamburger");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BurgerCreateDTO ekstraDto)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (ekstraDto.ImageFile != null)
                {
                    string uploadsDir = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + ekstraDto.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsDir, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ekstraDto.ImageFile.CopyToAsync(fileStream);
                    }
                }

                Hamburger ekstraMalzeme = new Hamburger()
                {
                    Adi = ekstraDto.Adi,
                    Fiyat = ekstraDto.Fiyat,
                    Resim = uniqueFileName.ToString(), // Burada dosya adını atıyoruz
                    Aciklama = ekstraDto.Aciklama
                };

                _HamburgerService.Create(ekstraDto);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                // ModelState geçerli değilse, aynı görünümü tekrar göster
                return View(ekstraDto);
            }
        }
        public IActionResult Edit(int id)
        {
            BurgerCreateDTO c = db.Burgerlar.Where(x => x.ID.Equals(id)).Select(x => new BurgerCreateDTO
            {
                Adi = x.Adi,
                Fiyat = x.Fiyat,

                Aciklama = x.Aciklama
            }).FirstOrDefault();
            return View(c);

        }
        [HttpPost]
        public IActionResult Edit(int id, BurgerCreateDTO dt)
        {
            Hamburger hamburger = db.Burgerlar.FirstOrDefault(x => x.ID.Equals(id));
            hamburger.Adi = dt.Adi;
            hamburger.Fiyat = dt.Fiyat;
            hamburger.Aciklama = dt.Aciklama;

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}