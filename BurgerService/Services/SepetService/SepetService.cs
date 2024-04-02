using BurgerDATA.Enum_s;
using BurgerREPO.Abstract;
using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.SepetService
{
    public class SepetService : ISepetService
    {
        private readonly ISepetREPO _sepetREPO;
        public SepetService(ISepetREPO sepetREPO)
        {
                _sepetREPO = sepetREPO;
        }
        public int Create(SepetCreateDTO model)
        {
            var sepet = new Sepet { Adi = model.Adi };
            return _sepetREPO.Create(sepet);
        }

        public async Task<int> Delete(int id)
        {
            var sepet = await _sepetREPO.GetByIdAsync(id);  //idsini verdiğim categoryi bulur.
            sepet.Status = Status.Deleted; //Durumunu deleted e çeker.
            sepet.SilmeZamani = DateTime.Now;
            return _sepetREPO.Delete(sepet);
        }

        public async  Task<List<Sepet>> GetAllSepet()
        {
            var sepets = await _sepetREPO.GetFilteredListAsync(select: x => new Sepet() { ID = x.ID, Adi = x.Adi, Status = x.Status,Fiyat=x.Fiyat }, orderBy: x => x.OrderBy(x => x.Adi));
            return sepets;
        }

        public async Task<List<Sepet>> GetAllSepetActive()
        {
            var sepets = await _sepetREPO.GetFilteredListAsync(select: x => new Sepet() { ID = x.ID, Adi = x.Adi, Status = x.Status }, where: x => x.Status != Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return sepets;
        }

        public async Task<Sepet> GetSepet(int id)
        {
            var sepet = await _sepetREPO.GetByIdAsync(id);
            return new Sepet() { ID = sepet.ID, Adi = sepet.Adi, Status = sepet.Status };
        }

        public int Update(SepetCreateDTO model)
        {
            var sepet = new Sepet() { Adi = model.Adi };
            sepet.Status = Status.Modified;
            sepet.GuncelleZamani = DateTime.Now;
            return _sepetREPO.Update(sepet);
        }

        public async Task<int> UpdateStatus(int id)
        {
            var sepet = await _sepetREPO.GetByIdAsync(id);
            if (sepet.Status == Status.Added)
                sepet.Status = Status.Deleted;
            else
                sepet.Status = Status.Added;

            return _sepetREPO.Update(sepet);
        }
        
    }
}
