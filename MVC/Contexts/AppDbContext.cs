
using DENEME.Concrete;
using Microsoft.EntityFrameworkCore;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE.DATA.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Menu> Menuler { get; set; }
        public DbSet<Burger> Burgerlar { get; set; }
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
        public DbSet<User> User { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // BurgerSiparis-Siparis ilişkisi
            modelBuilder.Entity<BurgerSiparis>()
                .HasOne(bs => bs.Siparis)
                .WithMany(s => s.BurgerSiparisler)
                .HasForeignKey(bs => bs.SiparisID)
                .OnDelete(DeleteBehavior.Restrict); // Siparis kaydını silmez, yalnızca BurgerSiparis kaydını siler

            // MenuSiparis-Siparis ilişkisi
            modelBuilder.Entity<MenuSiparis>()
                .HasOne(ms => ms.Siparis)
                .WithMany(s => s.MenuSiparisler)
                .HasForeignKey(ms => ms.SiparisID)
                .OnDelete(DeleteBehavior.Restrict);

            // EkstraMalzemeSiparis-Siparis ilişkisi
            modelBuilder.Entity<EkstraMalzemeSiparis>()
                .HasOne(ems => ems.Siparis)
                .WithMany(s => s.EkstraMalzemeSiparisler)
                .HasForeignKey(ems => ems.SiparisID)
                .OnDelete(DeleteBehavior.Restrict);

            // IcecekSiparis-Siparis ilişkisi
            modelBuilder.Entity<IcecekSiparis>()
                .HasOne(isp => isp.Siparis)
                .WithMany(s => s.IcecekSiparisler)
                .HasForeignKey(isp => isp.SiparisID)
                .OnDelete(DeleteBehavior.Restrict);

            // SosSiparis-Siparis ilişkisi
            modelBuilder.Entity<SosSiparis>()
                .HasOne(ss => ss.Siparis)
                .WithMany(s => s.SosSiparisler)
                .HasForeignKey(ss => ss.SiparisID)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
