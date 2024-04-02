﻿using DENEME.Concrete;
using MVCPROJE.ENTITIES.Enum_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE.ENTITIES.Concrete
{
    public class Sepet
    {
        public int ID { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public Boyut Boyut { get; set; }
        public ICollection<BurgerSiparis> BurgerSiparisler { get; set; }
        public ICollection<EkstraMalzemeSiparis> EkstraMalzemeSiparisler { get; set; }
        public ICollection<IcecekSiparis> IcecekSiparisler { get; set; }
        public ICollection<MenuSiparis> MenuSiparisler { get; set; }
        public User User { get; set; }

    }
}
