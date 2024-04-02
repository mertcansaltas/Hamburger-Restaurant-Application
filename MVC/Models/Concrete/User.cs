using MVCPROJE.ENTITIES.Abstract;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENEME.Concrete
{
    public class User:BaseEntity    
    {
        public int SepetId { get; set; }
        public Sepet Sepet { get; set; }
        public ICollection<Siparis> Siparis { get; set; }
    }
}
