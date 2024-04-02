using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE.ENTITIES.Concrete
{
    public class MenuSiparis
    {
        [Key]
        public int ID { get; set; }
        public int MenuID { get; set; }
        public int SiparisID { get; set; }
        public Menu Menu { get; set; }
        public Siparis Siparis { get; set; }
        public int SepetId { get; set; }
     
        public Sepet Sepet { get; set; }
    }
}
