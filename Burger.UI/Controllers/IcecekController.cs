using BurgerService.Services.IcecekService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.Controllers
{

    public class IcecekController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly IIcecekService _icecekservice;

        public IcecekController(IIcecekService icecekservice, UserManager<User> userManager, AppDbContext context)
        {
            _icecekservice = icecekservice;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ıceceks = await _icecekservice.GetAllIcecek();
            return View(ıceceks);

        }
        public async Task<IActionResult> SepeteIcecekEkle(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge(); // Kullanıcı girişi gerektir
            }

            var sepet = await _context.Sepetler.FindAsync(user.SepetId);
            var icecek = await _icecekservice.GetIcecek(id);    
            if (icecek == null)
            {
                return NotFound("İlgili içecek bulunamadı.");
            }

            // Sepete yeni bir içecek siparişi ekleyin
            var icecekSiparis = new IcecekSiparis
            {
                IcecekID = id,
                SepetId = sepet.ID,
                Adi = icecek.Adi,
                Fiyat=icecek.Fiyat



                // SiparisID alanı, bir sipariş tamamlandığında doldurulmalıdır
            };

            _context.IcecekSiparis.Add(icecekSiparis);
            await _context.SaveChangesAsync();
            sepet.IcecekSiparisler.Add(icecekSiparis);

            return RedirectToAction("Index","Icecek"); // Kullanıcıyı sepet sayfasına yönlendirin
        }
    }

    //public async Task<IActionResult> SepeteIcecekEkle(string icecekAdi, int sepetId)
    //{
    //    var user = await _userManager.GetUserAsync(User);
    //    if (user == null)
    //    {
    //        return Challenge(); // Kullanıcı girişi gerektir
    //    }
    //    // İsme göre bir Icecek nesnesi bulun
    //    var icecek = await _context.Icecekler
    //                               .FirstOrDefaultAsync(i => i.Adi == icecekAdi);

    //    if (icecek == null)
    //    {
    //        return NotFound();
    //    }

    //    // Bulunan Icecek nesnesinin ID'sini kullanarak yeni bir IcecekSiparis nesnesi oluşturun
    //    var icecekSiparis = new IcecekSiparis
    //    {
    //        IcecekID = icecek.ID, // Bulunan Icecek nesnesinin ID'si kullanılır
    //        SepetId = sepetId,
    //        // Diğer gerekli alanlar...
    //    };

    //    _context.IcecekSiparis.Add(icecekSiparis);
    //    await _context.SaveChangesAsync();

    //    return Ok();
    //}
}

