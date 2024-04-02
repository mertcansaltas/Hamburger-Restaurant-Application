using BurgerDATA.Enum_s;
using BurgerREPO.Abstract;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.EkstraMalzemeSiparisService
{
    public class EkstraMalzemeSiparisService : IEkstraMalzemeSiparisService
    {
        private readonly IEkstraMalzemeSiparisREPO _ekstraMalzemeSiparisREPO;
        public EkstraMalzemeSiparisService(IEkstraMalzemeSiparisREPO ekstraMalzemeSiparisRepo)
        {
            _ekstraMalzemeSiparisREPO = ekstraMalzemeSiparisRepo;
        }

        public async Task<int> DeletedStatus(int id)
        {
            var ekstra = await _ekstraMalzemeSiparisREPO.GetByIdAsync(id);
            ekstra.Status = Status.Deleted;

            return _ekstraMalzemeSiparisREPO.Update(ekstra);
        }

        public async Task<List<EkstraMalzemeSiparis>> GetAllEkstraMalzemeDeleted()
        {

            var ekstralar = await _ekstraMalzemeSiparisREPO.GetFilteredListAsync(select: x => new EkstraMalzemeSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat = x.EkstraMalzeme.Fiyat, OlusturmaZamani=x.EkstraMalzeme.OlusturmaZamani }, where: x => x.Status == Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return ekstralar;
        }

        public async Task<List<EkstraMalzemeSiparis>> GetAllEkstraMalzemeSiparis()
        {
            var ektramalzemeSiparis = await _ekstraMalzemeSiparisREPO.GetFilteredListAsync(select: x => new EkstraMalzemeSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat = x.EkstraMalzeme.Fiyat, OlusturmaZamani = x.EkstraMalzeme.OlusturmaZamani }, orderBy: x => x.OrderBy(x => x.Adi));
            return ektramalzemeSiparis;
        }

        public async Task<List<EkstraMalzemeSiparis>> GetAllEkstraMalzemeSiparisActive()
        {
            var ekstramalzemeSiparis = await _ekstraMalzemeSiparisREPO.GetFilteredListAsync(select: x => new EkstraMalzemeSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat = x.EkstraMalzeme.Fiyat, OlusturmaZamani = x.EkstraMalzeme.OlusturmaZamani }, where: x => x.Status != BurgerDATA.Enum_s.Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return ekstramalzemeSiparis;
        }

        public async Task<EkstraMalzemeSiparis> GetEkstraMalzemeSiparis(int id)
        {
            var ekstramalzemeSiparis = await _ekstraMalzemeSiparisREPO.GetByIdAsync(id);
            return new EkstraMalzemeSiparis() { ID = ekstramalzemeSiparis.ID, Adi = ekstramalzemeSiparis.Adi, Status = ekstramalzemeSiparis.Status };
        }
        public async Task<int> Delete(int id)
        {
            var icecek = await _ekstraMalzemeSiparisREPO.GetByIdAsync(id);  //idsini verdiğim categoryi bulur.
            icecek.Status = Status.Deleted; //Durumunu deleted e çeker.
            icecek.SilmeZamani = DateTime.Now;
            return _ekstraMalzemeSiparisREPO.Delete(icecek);
        }
    }
}
