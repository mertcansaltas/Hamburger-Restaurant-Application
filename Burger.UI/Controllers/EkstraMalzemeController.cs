using BurgerService.Services.EkstraMalzemeService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.Controllers
{
    public class EkstraMalzemeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly IEkstraMalzemeService _ekstraMalzemeService;

        public EkstraMalzemeController(UserManager<User> userManager, AppDbContext context, IEkstraMalzemeService ekstraMalzemeService)
        {
            _userManager = userManager;
            _context = context;
            _ekstraMalzemeService = ekstraMalzemeService;
        }

        public async Task<IActionResult> Index()
        {
            var ekstra = await _ekstraMalzemeService.GetAllEkstraMalzeme();
            return View(ekstra);
        }
        public async Task<IActionResult> SepeteEkstraMalzemeEkle(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge(); // Kullanıcı girişi gerektir
            }

            var sepet = await _context.Sepetler.FindAsync(user.SepetId);
            var ekstramalzeme = await _ekstraMalzemeService.GetEkstraMalzeme(id);
            if (ekstramalzeme == null)
            {
                return NotFound("İlgili ekstra malzeme bulunamadı.");
            }

            // Sepete yeni bir içecek siparişi ekleyin
            var ekstramalzemeSiparis = new EkstraMalzemeSiparis
            {
                EkstraMalzemeID = id,
                SepetId = sepet.ID,
                Adi = ekstramalzeme.Adi,
               


                // SiparisID alanı, bir sipariş tamamlandığında doldurulmalıdır
            };

            _context.EkstraMalzemeSiparis.Add(ekstramalzemeSiparis);
            await _context.SaveChangesAsync();
            sepet.EkstraMalzemeSiparisler.Add(ekstramalzemeSiparis);

            return RedirectToAction("Index", "EkstraMalzeme"); // Kullanıcıyı sepet sayfasına yönlendirin
        }
    }
}
