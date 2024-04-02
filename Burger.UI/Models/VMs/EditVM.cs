using BurgerDATA.Enum_s;

namespace Burger.UI.Models.VMs
{
    public class EditVM
    {
        public string Adi { get; set; }
        public string Resim { get; set; }
        public double Fiyat { get; set; }
        public string Aciklama { get; set; }
        public Status Status { get; set; }
    }
}
