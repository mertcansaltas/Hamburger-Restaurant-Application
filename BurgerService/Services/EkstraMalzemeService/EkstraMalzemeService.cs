using BurgerDATA.Enum_s;
using BurgerREPO.Abstract;
using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.EkstraMalzemeService
{
    public class EkstraMalzemeService : IEkstraMalzemeService
    {
        private readonly IEkstraMalzemeREPO _ekstraMalzemeREPO;
        public EkstraMalzemeService(IEkstraMalzemeREPO ekstraMalzemeREPO)
        {
            _ekstraMalzemeREPO = ekstraMalzemeREPO;
        }
        public int Create(EkstraMalzemeCreateDTO model)
        {
            var ekstraMalzeme = new EkstraMalzeme { Adi = model.Adi,Fiyat=model.Fiyat,Aciklama=model.Aciklama, Resim = model.ImageFile != null ? model.ImageFile.FileName : null };
            return _ekstraMalzemeREPO.Create(ekstraMalzeme);
        }

        public async Task<int> Delete(int id)
        {
            var ekstraMalzeme = await _ekstraMalzemeREPO.GetByIdAsync(id); 
            ekstraMalzeme.Status = Status.Deleted; 
            ekstraMalzeme.SilmeZamani = DateTime.Now;
            return _ekstraMalzemeREPO.Delete(ekstraMalzeme);
        }
        public  async Task<int> UpdateStatus(int id)
        {
            var ekstraMalzeme = await _ekstraMalzemeREPO.GetByIdAsync(id);
            if (ekstraMalzeme.Status == Status.Added)
                ekstraMalzeme.Status = Status.Deleted;
            else
                ekstraMalzeme.Status = Status.Added;

            return _ekstraMalzemeREPO.Update(ekstraMalzeme);
        }
        public async Task<List<EkstraMalzeme>> GetAllEkstraMalzeme()
        {
            var ektraMalzemelers = await _ekstraMalzemeREPO.GetFilteredListAsync(select: x => new EkstraMalzeme() { ID = x.ID, Adi = x.Adi, Status = x.Status,Fiyat=x.Fiyat,Aciklama=x.Aciklama,Resim=x.Resim }, orderBy: x => x.OrderBy(x => x.Adi));
            return ektraMalzemelers;
        }

        public async Task<List<EkstraMalzeme>> GetAllEkstraMalzemeActive()
        {
            var ektraMalzemeler = await _ekstraMalzemeREPO.GetFilteredListAsync(select: x => new EkstraMalzeme() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat=x.Fiyat }, where: x => x.Status != Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return ektraMalzemeler;
        }

        public async Task<EkstraMalzeme> GetEkstraMalzeme(int id)
        {
            var ekstramalzeme = await _ekstraMalzemeREPO.GetByIdAsync(id);
            return new EkstraMalzeme() { ID = ekstramalzeme.ID, Adi = ekstramalzeme.Adi, Status = ekstramalzeme.Status };
        }

        public int Update(EkstraMalzemeCreateDTO model)
        {
            var ektramalzeme = new EkstraMalzeme() { Adi = model.Adi };
            ektramalzeme.Status = Status.Modified;
            ektramalzeme.GuncelleZamani = DateTime.Now;
            return _ekstraMalzemeREPO.Update(ektramalzeme);
        }

      
    }
}
