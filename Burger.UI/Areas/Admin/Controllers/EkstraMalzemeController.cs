using BurgerREPO.Abstract;
using BurgerService.DTOs;
using BurgerService.Services.BurgerService;
using BurgerService.Services.EkstraMalzemeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EkstraMalzemeController : Controller
    {
        private readonly IEkstraMalzemeService _ekstraMalzeme;
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EkstraMalzemeController(IEkstraMalzemeService ekstraMalzeme, AppDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _ekstraMalzeme = ekstraMalzeme;
            this.db = db;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var ekstra = await _ekstraMalzeme.GetAllEkstraMalzeme();
            return View(ekstra);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var Icecek = await _ekstraMalzeme.GetEkstraMalzeme(id);
            if (Icecek != null)
                await _ekstraMalzeme.Delete(id);
            return RedirectToAction("Index", "EkstraMalzeme");
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EkstraMalzemeCreateDTO ekstraDto)
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

                EkstraMalzeme ekstraMalzeme = new EkstraMalzeme()
                {
                    Adi = ekstraDto.Adi,
                    Fiyat = ekstraDto.Fiyat,
                    Resim = uniqueFileName.ToString(), // Burada dosya adını atıyoruz
                    Aciklama = ekstraDto.Aciklama
                };

                _ekstraMalzeme.Create(ekstraDto);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                // ModelState geçerli değilse, aynı görünümü tekrar göster
                return View(ekstraDto);
            }
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekstraMalzeme = await db.EkstraMalzemeler.FindAsync(id);
            if (ekstraMalzeme == null)
            {
                return NotFound();
            }

            EkstraMalzemeCreateDTO ekstraMalzemeDTO = new EkstraMalzemeCreateDTO
            {
                Adi = ekstraMalzeme.Adi,
                Fiyat = ekstraMalzeme.Fiyat,
               
                Aciklama = ekstraMalzeme.Aciklama
            };

            return View(ekstraMalzemeDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EkstraMalzemeCreateDTO dt)
        {
            var ekstramal = await db.EkstraMalzemeler.FirstOrDefaultAsync(x => x.ID == id);
            if (ekstramal == null)
            {
                return NotFound();
            }

            string uniqueFileName = ekstramal.Resim; // Mevcut resim adını koru

            if (dt.ImageFile != null)
            {
                // Eski dosyayı silmek isteyebilirsiniz.
                // Yeni dosyayı kaydet
                string uploadsDir = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + dt.ImageFile.FileName;
                string filePath = Path.Combine(uploadsDir, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await dt.ImageFile.CopyToAsync(fileStream);
                }

                ekstramal.Resim = uniqueFileName; // Yeni dosya adını güncelle
            }

            ekstramal.Adi = dt.Adi;
            ekstramal.Fiyat = dt.Fiyat;
            ekstramal.Aciklama = dt.Aciklama;
            // Diğer alanları güncelle

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}