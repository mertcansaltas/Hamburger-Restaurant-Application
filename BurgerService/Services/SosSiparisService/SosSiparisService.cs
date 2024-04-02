using BurgerDATA.Enum_s;
using BurgerREPO.Abstract;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.SosSiparisService
{
    public class SosSiparisService : ISosSiparisService
    {
        private readonly ISosSiparisREPO _sossiparisREPO;
        public SosSiparisService(ISosSiparisREPO sossipraisREPO)
        {
            _sossiparisREPO = sossipraisREPO;
        }

        public async Task<int> DeletedStatus(int id)
        {
            var icecek = await _sossiparisREPO.GetByIdAsync(id);
            icecek.Status = Status.Deleted;

            return _sossiparisREPO.Update(icecek);
        }

        public async Task<List<SosSiparis>> GetAllSosSiparisDeleted()
        {

            var soslar = await _sossiparisREPO.GetFilteredListAsync(select: x => new SosSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat = x.Sos.Fiyat, OlusturmaZamani = x.Sos.OlusturmaZamani }, where: x => x.Status == Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return soslar;
        }

        public async Task<List<SosSiparis>> GetAllSosSiparis()
        {
            var sossiparis = await _sossiparisREPO.GetFilteredListAsync(select: x => new SosSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat = x.Sos.Fiyat, OlusturmaZamani = x.Sos.OlusturmaZamani }, orderBy: x => x.OrderBy(x => x.Adi));
            return sossiparis;
        }

        public async Task<List<SosSiparis>> GetAllSosSiparisActive()
        {
            var sossiparis = await _sossiparisREPO.GetFilteredListAsync(select: x => new SosSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat = x.Sos.Fiyat, OlusturmaZamani = x.Sos.OlusturmaZamani }, where: x => x.Status != Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return sossiparis;
        }

      

        public async Task<SosSiparis> GetSosSiparis(int id)
        {
            var sossiparis = await _sossiparisREPO.GetByIdAsync(id);
            return new SosSiparis() { ID = sossiparis.ID , Adi = sossiparis.Adi, Status = sossiparis.Status };
        }
        public async Task<int> Delete(int id)
        {
            var icecek = await _sossiparisREPO.GetByIdAsync(id);  //idsini verdiğim categoryi bulur.
            icecek.Status = Status.Deleted; //Durumunu deleted e çeker.
            icecek.SilmeZamani = DateTime.Now;
            return _sossiparisREPO.Delete(icecek);
        }
    }
}
