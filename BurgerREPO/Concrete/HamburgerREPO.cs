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
    public class HamburgerREPO : BaseREPO<Hamburger>, IHamburgerREPO
    {
        public HamburgerREPO(AppDbContext db) : base(db)
        {
        }
    }
}
