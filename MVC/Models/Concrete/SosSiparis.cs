using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE.ENTITIES.Concrete
{
    public class SosSiparis
    {
        [Key]
        public int ID { get; set; }
        public int SosID { get; set; }
        public int SiparisID { get; set; }
        public Sos? Sos { get; set; }
        public Siparis? Siparis { get; set; }
        public int SepetId { get; set; }
       
        public Sepet Sepet { get; set; }
    }
}
