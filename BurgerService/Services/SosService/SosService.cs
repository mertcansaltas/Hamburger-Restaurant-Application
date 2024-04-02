using BurgerDATA.Enum_s;
using BurgerREPO.Abstract;
using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.SosService
{
    public class SosService : ISosService
    {
        private readonly ISosREPO _sosRepo;
        public SosService(ISosREPO sosRepo)
        {
            _sosRepo = sosRepo;
        }
        public int Create(SosCreateDTO model)
        {
            var sos = new Sos { Adi = model.Adi };
            return _sosRepo.Create(sos);
        }

        public async Task<int> Delete(int id)
        {
            var sos = await _sosRepo.GetByIdAsync(id);  //idsini verdiğim categoryi bulur.
            sos.Status = Status.Deleted; //Durumunu deleted e çeker.
            sos.SilmeZamani = DateTime.Now;
            return _sosRepo.Delete(sos);
        }

        public async Task<List<Sos>> GetAllSos()
        {
            var soss = await _sosRepo.GetFilteredListAsync(select: x => new Sos() { Adi = x.Adi, Status = x.Status, Fiyat = x.Fiyat }, orderBy: x => x.OrderBy(x => x.Adi));
            return soss;
        }

        public async Task<List<Sos>> GetAllSosActive()
        {
            var soss = await _sosRepo.GetFilteredListAsync(select: x => new Sos() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat = x.Fiyat }, where: x => x.Status != Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return soss;
        }

        public async Task<Sos> GetSos(int id)
        {
            var sos = await _sosRepo.GetByIdAsync(id);
            return new Sos() { ID = sos.ID, Adi = sos.Adi, Status = sos.Status };
        }

        public int Update(SosCreateDTO model)
        {
            var sos = new Sos() { Adi = model.Adi,Fiyat=model.Fiyat };
            sos.Status = Status.Modified;
            sos.GuncelleZamani = DateTime.Now;
            return _sosRepo.Update(sos);
        }

        public async Task<int> UpdateStatus(int id)
        {
            var sos = await _sosRepo.GetByIdAsync(id);
            if (sos.Status == Status.Added)
                sos.Status = Status.Deleted;
            else
                sos.Status = Status.Added;

            return _sosRepo.Update(sos);
        }
    }
}
