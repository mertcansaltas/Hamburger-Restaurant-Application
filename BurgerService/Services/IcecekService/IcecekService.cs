using BurgerDATA.Enum_s;
using BurgerREPO.Abstract;
using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.IcecekService
{
    public class IcecekService : IIcecekService
    {
        private readonly IIcecekREPO _icecekREPO;
        public IcecekService(IIcecekREPO icecekREPO)
        {
            _icecekREPO = icecekREPO;
        }
        public int Create(IcecekCreateDTO model)
        {
            var icecek = new Icecek {Adi = model.Adi, Fiyat = model.Fiyat, Aciklama = model.Aciklama, Resim = model.ImageFile != null ? model.ImageFile.FileName : null };
            return _icecekREPO.Create(icecek);
        }

        public async Task<int> Delete(int id)
        {
            var icecek = await _icecekREPO.GetByIdAsync(id);  //idsini verdiğim categoryi bulur.
            icecek.Status = Status.Deleted; //Durumunu deleted e çeker.
            icecek.SilmeZamani = DateTime.Now;
            return _icecekREPO.Delete(icecek);
        }

        public async Task<List<Icecek>> GetAllIcecek()
        {
            var iceceks = await _icecekREPO.GetFilteredListAsync(select: x => new Icecek() { ID = x.ID, Adi = x.Adi, Status = x.Status,Fiyat=x.Fiyat,Resim = x.Resim }, orderBy: x => x.OrderBy(x => x.Adi));
            return iceceks;
        }

        public async Task<List<Icecek>> GetAllIcecekActive()
        {
            var iceceks = await _icecekREPO.GetFilteredListAsync(select: x => new Icecek() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat=x.Fiyat }, where: x => x.Status != Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return iceceks;
        }

        public async Task<Icecek> GetIcecek(int id)
        {
            var icecek = await _icecekREPO.GetByIdAsync(id);
            return new Icecek() { ID = icecek.ID, Adi = icecek.Adi, Status = icecek.Status };
        }

        public int Update(IcecekCreateDTO model)
        {
            var icecek = new Icecek() { Adi = model.Adi };
            icecek.Status = Status.Modified;
            icecek.GuncelleZamani = DateTime.Now;
            return _icecekREPO.Update(icecek);
        }

        public async Task<int> UpdateStatus(int id)
        {
            var icecek = await _icecekREPO.GetByIdAsync(id);
            if (icecek.Status == Status.Added)
                icecek.Status = Status.Deleted;
            else
                icecek.Status = Status.Added;

            return _icecekREPO.Update(icecek);
        }

       
    }
}
