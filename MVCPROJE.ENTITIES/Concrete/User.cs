
using Microsoft.AspNetCore.Identity;
using MVCPROJE.ENTITIES.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE.ENTITIES.Concrete
{
    public class User:IdentityUser
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Adres { get; set; }
        public int? SepetId { get; set; }
        public Sepet Sepet { get; set; }
        public ICollection<Siparis> Siparis { get; set; }
        public User()
        {
            Siparis=new HashSet<Siparis>(); 
        }
    }
}
