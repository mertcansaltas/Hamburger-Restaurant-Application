using Burger.UI.HelperClass;
using BurgerDATA.Concrete;
using BurgerREPO.Abstract;
using BurgerService.Services.BurgerService;
using BurgerService.Services.BurgerSiparisService;
using BurgerService.Services.EkstraMalzemeService;
using BurgerService.Services.EkstraMalzemeSiparisService;
using BurgerService.Services.IcecekService;
using BurgerService.Services.IcecekSiparisService;
using BurgerService.Services.MenuService;
using BurgerService.Services.MenuSiparisService;
using BurgerService.Services.SepetService;
using BurgerService.Services.SiparisService;
using BurgerService.Services.SosService;
using BurgerService.Services.SosSiparisService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Reflection;

namespace Burger.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Entity
            //builder.Services.AddDbContext<AppDbContext>(
            //  options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConn")));

            builder.Services.AddIdentity<User, Role>
               (options =>
               {
                   options.Password.RequireDigit = false;
                   options.Password.RequiredLength = 3;
                   options.Password.RequireLowercase = false;
                   options.Password.RequireUppercase = false;
                   options.Password.RequireNonAlphanumeric = false;

                   options.User.RequireUniqueEmail = false; //Ayný email adresiyle farklý kullanýcý oluþturamazsýn.

                   options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
               })
               .AddEntityFrameworkStores<AppDbContext>().AddRoleManager<RoleManager<Role>>(); // Bu satırı ekleyin

            builder.Services.AddDbContext<AppDbContext>();
            //Repo
            builder.Services.AddScoped<IBurgerSiparisREPO, BurgerSiparisREPO>();
            builder.Services.AddScoped<IEkstraMalzemeREPO, EkstraMalzemeREPO>();
            builder.Services.AddScoped<IEkstraMalzemeSiparisREPO, EkstraMalzemeSiparisREPO>();
            builder.Services.AddScoped<IHamburgerREPO, HamburgerREPO>();
            builder.Services.AddScoped<IIcecekREPO, IcecekREPO>();
            builder.Services.AddScoped<IIcecekSiparisREPO, IcecekSiparisREPO>();
            builder.Services.AddScoped<IMenuREPO, MenuREPO>();
            builder.Services.AddScoped<IMenuSiparisREPO, MenuSiparisREPO>();
            builder.Services.AddScoped<ISepetREPO, SepetREPO>();
            builder.Services.AddScoped<ISiparisREPO, SiparisREPO>();
            builder.Services.AddScoped<ISosREPO, SosREPO>();
            builder.Services.AddScoped<ISosSiparisREPO, SosSiparisREPO>();
            builder.Services.AddScoped<SepetiTemizle>();

            //Service
            builder.Services.AddScoped<IHamburgerService, HamburgerService>();
            builder.Services.AddScoped<IBurgerSiparisService, BurgerSiparisService>();
            builder.Services.AddScoped<IEkstraMalzemeService, EkstraMalzemeService>();
            builder.Services.AddScoped<IEkstraMalzemeSiparisService, EkstraMalzemeSiparisService>();
            builder.Services.AddScoped<IIcecekService, IcecekService>();
            builder.Services.AddScoped<IIcecekSiparisService, IcecekSiparisService>();
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<IMenuSiparisService, MenuSiparisService>();
            builder.Services.AddScoped<ISepetService, SepetService>();
            builder.Services.AddScoped<ISiparisService, SiparisService>();
            builder.Services.AddScoped<ISosService, SosService>();
            builder.Services.AddScoped<ISosSiparisService, SosSiparisService>();


            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/Account/Login";  //Giriþ yapýlaacak sayfa
            //    options.LogoutPath = " ";
            //});

            builder.Services.AddSession();

            var app = builder.Build();

            CreateRoles(app.Services).Wait();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseStatusCodePagesWithRedirects("/Error/ErrorPage/?{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            //app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapAreaControllerRoute(
               name: "AdminArea",
               areaName: "Admin",
                 pattern: "AdminArea/{controller=MenuBurger}/{action=Giris}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

           
               

            app.Run();
            static async Task CreateRoles(IServiceProvider serviceProvider)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>(); // Bu satırı düzelt

                    string[] roleNames = { "Admin", "User" };
                    foreach (var roleName in roleNames)
                    {
                        var roleExist = await roleManager.RoleExistsAsync(roleName);
                        if (!roleExist)
                        {
                            await roleManager.CreateAsync(new Role { Name = roleName }); // Role nesnesi oluşturma şeklini değiştir
                        }
                    }
                }
            }
        }
    }
}