using MVCPROJE.ENTITIES.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE.ENTITIES.Concrete
{
    public class BurgerSiparis:BaseEntity
    {
        
        public int BurgerID { get; set; }
        public int? SiparisID { get; set; }
        public Hamburger Burger { get; set; }
        public Siparis Siparis { get; set; }
        public int SepetId { get; set; }
        public Sepet Sepet { get; set; }
    }
}
