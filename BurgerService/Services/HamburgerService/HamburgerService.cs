using BurgerDATA.Enum_s;
using BurgerREPO.Abstract;
using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.BurgerService
{
    public class HamburgerService : IHamburgerService
    {
        private readonly IHamburgerREPO _burgerREPO;
        public HamburgerService(IHamburgerREPO burgerREPO)
        {
            _burgerREPO = burgerREPO;
        }
        public int Create(BurgerCreateDTO model)
        {
            var burger = new Hamburger { Adi = model.Adi, Fiyat = model.Fiyat, Aciklama = model.Aciklama, Resim = model.ImageFile != null ? model.ImageFile.FileName : null };
            return _burgerREPO.Create(burger);
        }

        public async Task<int> Delete(int id)
        {
            var burger = await _burgerREPO.GetByIdAsync(id);  //idsini verdiğim categoryi bulur.
            burger.Status = Status.Deleted; //Durumunu deleted e çeker.
            burger.SilmeZamani = DateTime.Now;
            return _burgerREPO.Delete(burger);

        }

        public  async Task<List<Hamburger>> GetAllBurger()
        {
            var burgers=await _burgerREPO.GetFilteredListAsync(select: x=> new Hamburger() { ID=x.ID, Adi=x.Adi,Fiyat=x.Fiyat , Status=x.Status,Aciklama=x.Aciklama,Resim=x.Resim}, orderBy: x=>x.OrderBy(x=>x.Adi));
            return burgers;
        }

        public async Task<List<Hamburger>> GetAllBurgerActive()
        {
            var burgers = await _burgerREPO.GetFilteredListAsync(select: x => new Hamburger() { ID = x.ID, Adi = x.Adi, Status = x.Status,Fiyat=x.Fiyat }, where: x => x.Status != Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return burgers;
        }

        public async Task<Hamburger> GetBurger(int id)
        {
            var burger = await _burgerREPO.GetByIdAsync(id);
            return new Hamburger() { ID = burger.ID, Adi = burger.Adi, Status = burger.Status,Fiyat=burger.Fiyat };
        }

        public int Update(BurgerCreateDTO model)
        {
            var burger = new Hamburger() { Adi = model.Adi };
            burger.Status = Status.Modified;
            burger.GuncelleZamani = DateTime.Now;
            return _burgerREPO.Update(burger);
        }

        public async Task<int> UpdateStatus(int id)
        {
            var burger = await _burgerREPO.GetByIdAsync(id);
            if (burger.Status == Status.Added)
                burger.Status = Status.Deleted;
            else
                burger.Status = Status.Added;

            return _burgerREPO.Update(burger);
        }
    }
}
