using BurgerDATA.Enum_s;
using BurgerREPO.Abstract;
using BurgerService.DTOs;
using Microsoft.EntityFrameworkCore;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.SiparisService
{
    public class SiparisService:ISiparisService
    {
        private readonly ISiparisREPO _siparisRepo;
        public SiparisService(ISiparisREPO siparisRepo)
        {
            _siparisRepo = siparisRepo;
        }
        public int Create(SiparisCreateDTO model)
        {
            var siparis = new Siparis { Adi = model.Adi };
            return _siparisRepo.Create(siparis);
        }

        public async Task<int> Delete(int id)
        {
            var siparis = await _siparisRepo.GetByIdAsync(id);  //idsini verdiğim categoryi bulur.
            siparis.Status = Status.Deleted; //Durumunu deleted e çeker.
            siparis.SilmeZamani = DateTime.Now;
            return _siparisRepo.Delete(siparis);
        }

        public async Task<List<Siparis>> GetAllSiparis()
        {
            var siparis = await _siparisRepo.GetFilteredListAsync(select: x => new Siparis() { ID = x.ID, Adi = x.Adi, Status = x.Status }, orderBy: x => x.OrderBy(x => x.Adi));
            return siparis;
        }

        public async Task<List<Siparis>> GetAllSiparisActive()
        {
            var siparis = await _siparisRepo.GetFilteredListAsync(select: x => new Siparis() { ID = x.ID, Adi = x.Adi, Status = x.Status }, where: x => x.Status != Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return siparis;
        }

        public async Task<Siparis> GetSiparis(int id)
        {
            var siparis = await _siparisRepo.GetByIdAsync(id);
            return new Siparis() { ID = siparis.ID, Adi = siparis.Adi, Status = siparis.Status };
        }

        public int Update(SiparisCreateDTO model)
        {
            var siparis = new Siparis() { Adi = model.Adi };
            siparis.Status = Status.Modified;
            siparis.GuncelleZamani = DateTime.Now;
            return _siparisRepo.Update(siparis);
        }

        public async Task<int> UpdateStatus(int id)
        {
            var siparis = await _siparisRepo.GetByIdAsync(id);
            if (siparis.Status == Status.Added)
                siparis.Status = Status.Deleted;
            else
                siparis.Status = Status.Added;

            return _siparisRepo.Update(siparis);
        }

        public async Task<List<Siparis>> GetKullaniciSiparislerAsync(string kullaniciAdi)
        {
            return await _siparisRepo.GetWhereAsync(x=>x.Adi==kullaniciAdi);
        }
    }
}
