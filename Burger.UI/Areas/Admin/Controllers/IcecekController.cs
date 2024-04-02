using BurgerService.DTOs;
using BurgerService.Services.IcecekService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class IcecekController : Controller
    {
        private readonly IIcecekService _icecekservice;
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public IcecekController(IIcecekService icecekservice, AppDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _icecekservice = icecekservice;
            this.db = db;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var ıceceks = await _icecekservice.GetAllIcecek();
            return View(ıceceks);

        }
        public async Task<IActionResult> Delete(int id)
        {
            var Icecek = await _icecekservice.GetIcecek(id);
            if (Icecek != null)
                await _icecekservice.Delete(id);
            return RedirectToAction("Index", "Icecek");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IcecekCreateDTO ekstraDto)
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

                Icecek icecek = new Icecek()
                {
                    Adi = ekstraDto.Adi,
                    Fiyat = ekstraDto.Fiyat,
                    Resim = uniqueFileName.ToString(), // Burada dosya adını atıyoruz
                    Aciklama = ekstraDto.Aciklama
                };

                _icecekservice.Create(ekstraDto);

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
            IcecekCreateDTO c = db.Icecekler.Where(x => x.ID.Equals(id)).Select(x => new IcecekCreateDTO
            {
                Adi = x.Adi,
                Fiyat = x.Fiyat,

                Aciklama = x.Aciklama
            }).FirstOrDefault();
            return View(c);

        }
        [HttpPost]
        public IActionResult Edit(int id, IcecekCreateDTO dt)
        {
            Icecek ıcecek = db.Icecekler.FirstOrDefault(x => x.ID.Equals(id));
            ıcecek.Adi = dt.Adi;
            ıcecek.Fiyat = dt.Fiyat;
            ıcecek.Aciklama = dt.Aciklama;

            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}