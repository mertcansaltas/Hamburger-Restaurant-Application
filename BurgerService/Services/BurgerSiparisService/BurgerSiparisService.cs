using BurgerDATA.Enum_s;
using BurgerREPO.Abstract;
using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.BurgerSiparisService
{
    public class BurgerSiparisService : IBurgerSiparisService
    {
        private readonly IBurgerSiparisREPO _burgerSiparisREPO;

        public BurgerSiparisService(IBurgerSiparisREPO burgerSiparisREPO)
        {
            _burgerSiparisREPO = burgerSiparisREPO;
        }
  

        public async Task<List<BurgerSiparis>> GetAllBurgerSiparis()
        {
            var burgerssiparis = await _burgerSiparisREPO.GetFilteredListAsync(select: x => new BurgerSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status,Fiyat=x.Burger.Fiyat,OlusturmaZamani=x.Burger.OlusturmaZamani }, orderBy: x => x.OrderBy(x => x.Adi));
            return burgerssiparis;
        }

        public async Task<List<BurgerSiparis>> GetAllBurgerSiparisActive()
        {
            var burgersSiparis = await _burgerSiparisREPO.GetFilteredListAsync(select: x => new BurgerSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat = x.Burger.Fiyat, OlusturmaZamani = x.Burger.OlusturmaZamani }, where: x => x.Status != Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return burgersSiparis;
        }

        public async Task<BurgerSiparis> GetBurgerSiparis(int id)
        {
            var burgerSiparis = await _burgerSiparisREPO.GetByIdAsync(id);
            return new BurgerSiparis() { ID = burgerSiparis.ID, Adi = burgerSiparis.Adi, Status = burgerSiparis.Status, Fiyat =burgerSiparis.Burger.Fiyat, OlusturmaZamani = burgerSiparis.Burger.OlusturmaZamani };
        }

        public async Task<List<BurgerSiparis>> GetAllBurgerSiparisDeleted()
        {
            var burgers = await _burgerSiparisREPO.GetFilteredListAsync(select: x => new BurgerSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat = x.Burger.Fiyat, OlusturmaZamani = x.Burger.OlusturmaZamani }, where: x => x.Status == Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return burgers;
        }

        public async Task<int> DeletedStatus(int id)
        {
            var burger = await _burgerSiparisREPO.GetByIdAsync(id);
            burger.Status = Status.Deleted;

            return _burgerSiparisREPO.Update(burger);
        }
        public async Task<int> Delete(int id)
        {
            var icecek = await _burgerSiparisREPO.GetByIdAsync(id);  //idsini verdiğim categoryi bulur.
            icecek.Status = Status.Deleted; //Durumunu deleted e çeker.
            icecek.SilmeZamani = DateTime.Now;
            return _burgerSiparisREPO.Delete(icecek);
        }
    }
}
