using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.SiparisService
{
    public interface ISiparisService
    {
        int Create(SiparisCreateDTO model);
        int Update(SiparisCreateDTO model);
        Task<int> UpdateStatus(int id);
        Task<int> Delete(int id);
        Task<List<Siparis>> GetAllSiparis();
        Task<List<Siparis>> GetAllSiparisActive();
        Task<Siparis> GetSiparis(int id);
        Task<List<Siparis>> GetKullaniciSiparislerAsync(string kullaniciAdi);
    }
}
