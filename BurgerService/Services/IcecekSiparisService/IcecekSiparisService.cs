using BurgerDATA.Enum_s;
using BurgerREPO.Abstract;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.IcecekSiparisService
{
    public class IcecekSiparisService : IIcecekSiparisService
    {
        private readonly IIcecekSiparisREPO _icecekSiparisRepo;
        public IcecekSiparisService(IIcecekSiparisREPO icecekSiparisRepo)
        {
            _icecekSiparisRepo = icecekSiparisRepo;
        }
        public async Task<List<IcecekSiparis>> GetAllIcecekSiparis()
        {
            var icecekSiparisler = await _icecekSiparisRepo.GetFilteredListAsync(select: x => new IcecekSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status,Fiyat=x.Icecek.Fiyat, OlusturmaZamani = x.Icecek.OlusturmaZamani }, orderBy: x => x.OrderBy(x => x.Adi));
            return icecekSiparisler;
        }

        public async Task<List<IcecekSiparis>> GetAllIcecekSiparisActive()
        {
            var iceceksiparis = await _icecekSiparisRepo.GetFilteredListAsync(select: x => new IcecekSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat=x.Icecek.Fiyat, OlusturmaZamani = x.Icecek.OlusturmaZamani }, where: x => x.Status != Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return iceceksiparis;
        }

        public async Task<IcecekSiparis> GetIcecekSiparis(int id)
        {
            var icecekSiparis = await _icecekSiparisRepo.GetByIdAsync(id);
            return new IcecekSiparis() { ID = icecekSiparis.ID, Adi = icecekSiparis.Adi, Status = icecekSiparis.Status,Fiyat=icecekSiparis.Fiyat };
        }

        public async Task<int> Delete(int id)
        {
            var icecek = await _icecekSiparisRepo.GetByIdAsync(id);  //idsini verdiğim categoryi bulur.
            icecek.Status = Status.Deleted; //Durumunu deleted e çeker.
            icecek.SilmeZamani = DateTime.Now;
            return _icecekSiparisRepo.Delete(icecek);
        }

        public async Task<List<IcecekSiparis>> GetAllIcecekDeleted()
        {
            var iceceks = await _icecekSiparisRepo.GetFilteredListAsync(select: x => new IcecekSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status,Fiyat=x.Icecek.Fiyat, OlusturmaZamani = x.Icecek.OlusturmaZamani }, where: x => x.Status == Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return iceceks;
        }

        public async Task<int> DeletedStatus(int id)
        {
            var icecek = await _icecekSiparisRepo.GetByIdAsync(id);
            icecek.Status = Status.Deleted;

            return  _icecekSiparisRepo.Update(icecek);
        }
    }
}
