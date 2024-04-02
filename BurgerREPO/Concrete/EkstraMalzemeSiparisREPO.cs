using BurgerREPO.Concrete;
using MVCPROJE.DATA.Contexts;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerREPO.Abstract
{
    public class EkstraMalzemeSiparisREPO : BaseREPO<EkstraMalzemeSiparis>, IEkstraMalzemeSiparisREPO
    {
        public EkstraMalzemeSiparisREPO(AppDbContext db) : base(db)
        {
        }
    }
}
