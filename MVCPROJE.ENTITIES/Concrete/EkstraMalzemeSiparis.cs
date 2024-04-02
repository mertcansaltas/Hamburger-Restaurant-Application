using MVCPROJE.ENTITIES.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE.ENTITIES.Concrete
{
    public class EkstraMalzemeSiparis:BaseEntity
    {
     
        public int EkstraMalzemeID { get; set; }
        public int? SiparisID { get; set; }
        public EkstraMalzeme EkstraMalzeme { get; set; }
        public Siparis Siparis { get; set; }
        public int SepetId { get; set; }
        public Sepet Sepet { get; set; }
    }
}
