using BurgerService.Services.MenuService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.Controllers
{
    public class MenulerController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;

        public MenulerController(IMenuService menuService, UserManager<User> userManager, AppDbContext context)
        {
            _menuService = menuService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var menuler = await _menuService.GetAllMenu();
            return View(menuler);
        }
        public async Task<IActionResult> SepeteMenuEkle(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge(); // Kullanıcı girişi gerektir
            }

            var sepet = await _context.Sepetler.FindAsync(user.SepetId);
            var menu = await _menuService.GetMenu(id);
            if (menu == null)
            {
                return NotFound("İlgili menü bulunamadı.");
            }

            // Sepete yeni bir içecek siparişi ekleyin
            var menuSiparis = new MenuSiparis
            {
                MenuID = id,
                SepetId = sepet.ID,
                Adi = menu.Adi,
                Fiyat=menu.Fiyat



                // SiparisID alanı, bir sipariş tamamlandığında doldurulmalıdır
            };

            _context.MenuSiparis.Add(menuSiparis);
            await _context.SaveChangesAsync();
            sepet.MenuSiparisler.Add(menuSiparis);

            return RedirectToAction("Index", "Menuler"); // Kullanıcıyı sepet sayfasına yönlendirin
        }


    }
}
