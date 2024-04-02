using BurgerDATA.Enum_s;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE.ENTITIES.Abstract
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Ad Alanı Boş Olamaz!")]
        public string Adi { get; set; } = null!;
        [Required(ErrorMessage = "Fiyat Alanı Boş Olamaz!")]

        public double Fiyat { get; set; }
      
        public string? Resim { get; set; } = null!;

        public string? Aciklama { get; set; }

        public DateTime OlusturmaZamani { get; set; } = DateTime.Now;
        public DateTime? GuncelleZamani { get; set; }
        public DateTime? SilmeZamani { get; set; }
        public Status Status { get; set; }
    }
}
