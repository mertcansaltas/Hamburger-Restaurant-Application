using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.DTOs
{
    public class SiparisCreateDTO
    {

        [Required(ErrorMessage = "Ad Alanı Boş Olamaz!")]
        public string Adi { get; set; } = null!;
        [Required(ErrorMessage = "Fiyat Alanı Boş Olamaz!")]

        public double Fiyat { get; set; }

        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Açıklama Alanı Boş Olamaz!")]

        public string Aciklama { get; set; } = null!;
    }
}
