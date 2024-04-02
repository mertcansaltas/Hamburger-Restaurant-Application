    using Burger.UI.Models.VM_s;
    using BurgerDATA.Concrete;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
using MVCPROJE.DATA.Contexts;
    using MVCPROJE.ENTITIES.Concrete;

    namespace Burger.UI.Controllers
    {
        public class AccountController : Controller
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly AppDbContext _dbContext; // Örnek bir DbContext ismi

            public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext dbContext)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _dbContext = dbContext;
            }

            // Kayıt olma view'ını gösterir
            public IActionResult Register()
            {
                return View();
            }


            [HttpPost]
            public async Task<IActionResult> Register(RegisterVM model)
            {
                if (ModelState.IsValid)
                {
                    var user = new User
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        Ad = model.Ad,
                        Soyad = model.Soyad,
                        Adres = model.Adres
                        // Diğer gereken kullanıcı bilgileri...
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        // Rol ataması yapılıyor
                        var roleResult = model.Email == "admin@admin.com"
                            ? await _userManager.AddToRoleAsync(user, "Admin")
                            : await _userManager.AddToRoleAsync(user, "User");

                        if (!roleResult.Succeeded)
                        {
                            // Rol atamasında hata oluşursa, hataları ModelState'e ekle
                            foreach (var error in roleResult.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                            // Kullanıcı oluşturuldu ama rol ataması başarısız oldu, bu yüzden kullanıcıyı sil
                            await _userManager.DeleteAsync(user);
                            return View(model);
                        }

                        var sepet = new Sepet
                        {
                            Adi = "Sepet",
                            Fiyat = 0,
                            Adet = 0,
                        };
                        _dbContext.Sepetler.Add(sepet);
                        await _dbContext.SaveChangesAsync();

                        user.SepetId = sepet.ID;
                        await _userManager.UpdateAsync(user);

                        // Kullanıcıyı otomatik giriş yaptır
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Giris", "MenuBurger", new { area = "Admin" });// Admin controllerının Index action'ına yönlendir.
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home"); // Home controllerının Index action'ına yönlendir.
                    }
                    //return RedirectToAction("Index", "Home");
                }

                    // Kullanıcı oluşturma işlemi başarısız oldu, hataları ModelState'e ekle
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                // ModelState geçerli değilse veya başka bir hata oluştuysa, formu tekrar göster
                return View(model);
            }

            // Giriş yapma view'ını gösterir
            public IActionResult Login()
            {
                return View();
            }

            // Giriş yapma işlemini gerçekleştirir
            [HttpPost]
            public async Task<IActionResult> Login(LoginVM vm)
            {
            if (ModelState.IsValid)
            {
                User kullanici = await _userManager.FindByEmailAsync(vm.Email);
                if (kullanici != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(kullanici, vm.Password, false, false);
                    if (signInResult.Succeeded)
                    {

                        if (await _userManager.IsInRoleAsync(kullanici, "Admin"))
                        {
                            return RedirectToAction("Giris", "MenuBurger", new { area = "Admin" });//Gideceği yer..
                        }
                        else
                        {
                            return RedirectToAction("Index", "Kullanici"); //Gideceği yer..
                        }
                    }
                    ModelState.AddModelError("", "Wrong credantion information");
                }
            }
            return View(vm);
        }


            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
      

    }
}
