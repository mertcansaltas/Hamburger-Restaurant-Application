using BurgerDATA.Enum_s;
using BurgerREPO.Abstract;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.MenuSiparisService
{
    public class MenuSiparisService : IMenuSiparisService
    {
        private readonly IMenuSiparisREPO _menusiparisREPO;

        public MenuSiparisService(IMenuSiparisREPO  menusiparisREPO)
        {
            _menusiparisREPO = menusiparisREPO;
        }

        public async Task<int> DeletedStatus(int id)
        {
            var menu = await _menusiparisREPO.GetByIdAsync(id);
            menu.Status = Status.Deleted;

            return _menusiparisREPO.Update(menu);
        }

        public async Task<List<MenuSiparis>> GetAllMenuSiparisDeleted()
        {
            var menuler = await _menusiparisREPO.GetFilteredListAsync(select: x => new MenuSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat=x.Menu.Fiyat, OlusturmaZamani = x.Menu.OlusturmaZamani }, where: x => x.Status == Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return menuler;
        }

        public async Task<List<MenuSiparis>> GetAllMenuSiparis()
        {
            var menusiparis = await _menusiparisREPO.GetFilteredListAsync(select: x => new MenuSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status,Fiyat=x.Menu.Fiyat, OlusturmaZamani = x.Menu.OlusturmaZamani }, orderBy: x => x.OrderBy(x => x.Adi));
            return menusiparis;
        }

        public async Task<List<MenuSiparis>> GetAllMenuSiparisActive()
        {
            var menusiparis = await _menusiparisREPO.GetFilteredListAsync(select: x => new MenuSiparis() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat=x.Menu.Fiyat, OlusturmaZamani = x.Menu.OlusturmaZamani }, where: x => x.Status != Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return menusiparis;
        }

        public async Task<MenuSiparis> GetMenuSiparis(int id)
        {
            var menusiparis = await _menusiparisREPO.GetByIdAsync(id);
            return new MenuSiparis() { ID = menusiparis.ID, Adi = menusiparis.Adi, Status = menusiparis.Status };
        }
        public async Task<int> Delete(int id)
        {
            var icecek = await _menusiparisREPO.GetByIdAsync(id);  //idsini verdiğim categoryi bulur.
            icecek.Status = Status.Deleted; //Durumunu deleted e çeker.
            icecek.SilmeZamani = DateTime.Now;
            return _menusiparisREPO.Delete(icecek);
        }
    }
}
