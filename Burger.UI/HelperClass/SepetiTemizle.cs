using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.HelperClass
{
    public class SepetiTemizle
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;

        public SepetiTemizle(AppDbContext dbContext, UserManager<User> userManager)
        {
            _context = dbContext;
            _userManager = userManager;
        }

        public async Task Temizle(HttpContext httpContext)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);

            var sepet = await _context.Sepetler
                                      .Include(s => s.BurgerSiparisler)
                                      .Include(s => s.IcecekSiparisler)
                                      .Include(s => s.MenuSiparisler)
                                      .Include(s => s.SosSiparisler)
                                      .Include(s => s.EkstraMalzemeSiparisler)
                                      .FirstOrDefaultAsync(s => s.User.Id == user.Id);

            if (sepet != null)
            {
                // Sepetteki tüm ürünleri sil
                _context.BurgerSiparis.RemoveRange(sepet.BurgerSiparisler);
                _context.IcecekSiparis.RemoveRange(sepet.IcecekSiparisler);
                _context.MenuSiparis.RemoveRange(sepet.MenuSiparisler);
                _context.SosSiparis.RemoveRange(sepet.SosSiparisler);
                _context.EkstraMalzemeSiparis.RemoveRange(sepet.EkstraMalzemeSiparisler);

                await _context.SaveChangesAsync();
            }          
        }
    }
}
