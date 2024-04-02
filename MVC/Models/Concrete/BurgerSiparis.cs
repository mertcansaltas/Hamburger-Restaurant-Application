using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE.ENTITIES.Concrete
{
    public class BurgerSiparis
    {
        [Key]
        public int ID { get; set; }
        public int BurgerID { get; set; }
        public int SiparisID { get; set; }
        public Burger Burger { get; set; }
    
        public Siparis Siparis { get; set; }
        public int SepetId { get; set; }
        public Sepet Sepet { get; set; }
    }
}
