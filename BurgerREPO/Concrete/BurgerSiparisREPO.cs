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
    public class BurgerSiparisREPO : BaseREPO<BurgerSiparis>, IBurgerSiparisREPO
    {
        public BurgerSiparisREPO(AppDbContext db) : base(db)
        {

        }
    }
}
