using MVCPROJE.ENTITIES.Abstract;
using MVCPROJE.ENTITIES.Enum_s;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE.ENTITIES.Concrete
{
    public class IcecekSiparis:BaseEntity
    {
        //[Key]
        //public int ID { get; set; }
        public int IcecekID { get; set; }
        public int? SiparisID { get; set; }
        public Icecek? Icecek { get; set; }
        public Siparis? Siparis { get; set; }
        public int SepetId { get; set; }
        public Sepet Sepet { get; set; }
        public Boyut Boyut { get; set; }
    }
}
