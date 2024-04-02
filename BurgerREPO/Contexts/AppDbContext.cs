
using BurgerDATA.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE.DATA.Contexts
{
    public class AppDbContext : IdentityDbContext<User,Role,string>
    {
        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        //{

        //}

        public DbSet<Menu> Menuler { get; set; }
        public DbSet<Hamburger> Burgerlar { get; set; }
        public DbSet<Icecek> Icecekler { get; set; }
        public DbSet<EkstraMalzeme> EkstraMalzemeler { get; set; }
        public DbSet<Sos> Soslar { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<BurgerSiparis> BurgerSiparis { get; set; }
        public DbSet<EkstraMalzemeSiparis> EkstraMalzemeSiparis { get; set; }
        public DbSet<IcecekSiparis> IcecekSiparis { get; set; }
        public DbSet<MenuSiparis> MenuSiparis { get; set; }
        public DbSet<SosSiparis> SosSiparis { get; set; }
        public DbSet<Sepet> Sepetler { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-M2EMNTI;Database=MVCPazarSon;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=true;TrustServerCertificate=True");
        }
        
    }
}
