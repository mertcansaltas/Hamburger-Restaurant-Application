using MVCPROJE.ENTITIES.Concrete;

namespace Burger.UI.Models.VMs
{
    public class AdminVM
    {
        public List<Hamburger> Hamburgerler{ get; set; }
        public Hamburger Hamburger{ get; set; }
        public Menu Menu { get; set; }
        public List<Menu> Menuler { get; set; }
        public List<Icecek> Icecekler { get; set; }
        public Icecek Icecek { get; set; }
        public List<Sos> Soslar { get; set; }
        public Sos Sos { get; set; }
        public EkstraMalzeme EkstraMalzeme { get; set; }
        public List<EkstraMalzeme> EkstraMalzemeler { get; set; }
    }
}
