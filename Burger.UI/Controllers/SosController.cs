using BurgerService.Services.SosService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.Controllers
{
    public class SosController : Controller
    {
        private readonly ISosService _sosService;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;

        public SosController(ISosService sosService, UserManager<User> userManager, AppDbContext context)
        {
            _sosService = sosService;
            _userManager = userManager;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var sos = await _sosService.GetAllSos();
            return View(sos);
        }
        public async Task<IActionResult> SepeteSosEkle(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge(); // Kullanıcı girişi gerektir
            }

            var sepet = await _context.Sepetler.FindAsync(user.SepetId);
            var sos = await _sosService.GetSos(id);
            if (sos == null)
            {
                return NotFound("İlgili içecek bulunamadı.");
            }

            // Sepete yeni bir içecek siparişi ekleyin
            var sosSiparis = new SosSiparis()
            {
                SosID = id,
                SepetId = sepet.ID,
                Adi = sos.Adi,


                // SiparisID alanı, bir sipariş tamamlandığında doldurulmalıdır
            };

            _context.SosSiparis.Add(sosSiparis);
            await _context.SaveChangesAsync();
            sepet.SosSiparisler.Add(sosSiparis);

            return RedirectToAction("Index", "Sos"); // Kullanıcıyı sepet sayfasına yönlendirin
        }
    }
}
