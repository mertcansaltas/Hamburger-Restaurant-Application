using MVCPROJE.ENTITIES.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE.ENTITIES.Concrete
{
    public class EkstraMalzeme:BaseEntity
    {
        public ICollection<EkstraMalzemeSiparis> EkstraMalzemeSiparis { get; set; }

       
    }
}
