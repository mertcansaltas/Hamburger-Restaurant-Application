using BurgerService.DTOs;
using BurgerService.Services.MenuService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenuBurgerController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MenuBurgerController(IMenuService menuService, AppDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _menuService = menuService;
            this.db = db;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var menuler = await _menuService.GetAllMenu();
            return View(menuler);
        }

        public IActionResult Giris()
        {
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var Icecek = await _menuService.GetMenu(id);
            if (Icecek != null)
                await _menuService.Delete(id);
            return RedirectToAction("Index", "MenuBurger");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MenuCreateDTO ekstraDto)
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

                Menu menu = new Menu()
                {
                    Adi = ekstraDto.Adi,
                    Fiyat = ekstraDto.Fiyat,
                    Resim = uniqueFileName.ToString(), // Burada dosya adını atıyoruz
                    Aciklama = ekstraDto.Aciklama
                };

                _menuService.Create(ekstraDto);

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
            MenuCreateDTO c = db.Menuler.Where(x => x.ID.Equals(id)).Select(x => new MenuCreateDTO
            {
                Adi = x.Adi,
                Fiyat = x.Fiyat,

                Aciklama = x.Aciklama
            }).FirstOrDefault();
            return View(c);

        }
        [HttpPost]
        public IActionResult Edit(int id, MenuCreateDTO dt)
        {
            Menu menu = db.Menuler.FirstOrDefault(x => x.ID.Equals(id));
            menu.Adi = dt.Adi;
            menu.Fiyat = dt.Fiyat;
            menu.Aciklama = dt.Aciklama;

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

