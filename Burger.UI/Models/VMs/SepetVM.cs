using MVCPROJE.ENTITIES.Concrete;
using MVCPROJE.ENTITIES.Enum_s;
using System.ComponentModel.DataAnnotations;

namespace Burger.UI.Models.VMs
{
    public class SepetVM
    {
        public List<IcecekSiparis> IcecekSiparisler { get; set; }
        public List<MenuSiparis> MenuSiparisler { get; set; }
        public List<BurgerSiparis> BurgerSiparisler { get; set; }
        public List<SosSiparis> SosSiparisler { get; set; }
        public List<EkstraMalzemeSiparis> EkstraMalzemeSiparisler { get; set; }
        public List<Sepet> Sepetler { get; set; }
        public string Adi { get; set; }

        public double Fiyat { get; set; }

        public string Resim { get; set; }

        public string Aciklama { get; set; }
        public int Adet { get; set; }
        public Sepet Sepet { get; set; }

        [Key]
        public int Id { get; set; }
        public Boyut Boyut { get; set; }


    }
}
