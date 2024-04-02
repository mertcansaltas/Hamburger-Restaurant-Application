using BurgerService.Services.BurgerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.Controllers
{
    public class HamburgerController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly IHamburgerService _hamburgerService;


        public HamburgerController(UserManager<User> userManager, AppDbContext context, IHamburgerService hamburgerService)
        {
            _userManager = userManager;
            _context = context;
            _hamburgerService = hamburgerService;
        }

        public async Task<IActionResult> Index()
        {
            var hamburgers = await _hamburgerService.GetAllBurger();
            return View(hamburgers);
        }
        public async Task<IActionResult> SepeteHamburgerEkle(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge(); // Kullanıcı girişi gerektir
            }

            var sepet = await _context.Sepetler.FindAsync(user.SepetId);
            var hambuger = await _hamburgerService.GetBurger(id);
            if (hambuger == null)
            {
                return NotFound("İlgili hamburger bulunamadı.");
            }

            // Sepete yeni bir içecek siparişi ekleyin
            var hamburgerSiparis = new BurgerSiparis()
            {
                BurgerID = id,
                SepetId = sepet.ID,
                Adi = hambuger.Adi,


                // SiparisID alanı, bir sipariş tamamlandığında doldurulmalıdır
            };

            _context.BurgerSiparis.Add(hamburgerSiparis);
            await _context.SaveChangesAsync();
            sepet.BurgerSiparisler.Add(hamburgerSiparis);

            return RedirectToAction("Index", "Hamburger"); // Kullanıcıyı sepet sayfasına yönlendirin
        }
    }
}
