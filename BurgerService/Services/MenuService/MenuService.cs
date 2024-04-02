using BurgerDATA.Enum_s;
using BurgerREPO.Abstract;
using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.MenuService
{
    public class MenuService : IMenuService
    {
        private readonly IMenuREPO _menuREPO;
        public MenuService(IMenuREPO menuREPO)
        {
            _menuREPO = menuREPO;
        }
        public int Create(MenuCreateDTO model)
        {
            var menu = new Menu { Adi = model.Adi, Fiyat = model.Fiyat, Aciklama = model.Aciklama, Resim = model.ImageFile != null ? model.ImageFile.FileName : null };
            return _menuREPO.Create(menu);
        }

        public async Task<int> Delete(int id)
        {
            var menu = await _menuREPO.GetByIdAsync(id);  //idsini verdiğim categoryi bulur.
            menu.Status = Status.Deleted; //Durumunu deleted e çeker.
            menu.SilmeZamani = DateTime.Now;
            return _menuREPO.Delete(menu);
        }

        public async Task<List<Menu>> GetAllMenu()
        {
            var menus = await _menuREPO.GetFilteredListAsync(select: x => new Menu() { ID = x.ID, Adi = x.Adi, Status = x.Status,Fiyat=x.Fiyat,Resim=x.Resim }, orderBy: x => x.OrderBy(x => x.Adi));
            return menus;
        }

        public async Task<List<Menu>> GetAllMenuActive()
        {
            var menus = await _menuREPO.GetFilteredListAsync(select: x => new Menu() { ID = x.ID, Adi = x.Adi, Status = x.Status, Fiyat=x.Fiyat }, where: x => x.Status != Status.Deleted, orderBy: x => x.OrderBy(x => x.Adi));
            return menus;
        }

        public async Task<Menu> GetMenu(int id)
        {
            var menu = await _menuREPO.GetByIdAsync(id);
            return new Menu() { ID = menu.ID, Adi = menu.Adi, Status = menu.Status };
        }

        public int Update(MenuCreateDTO model)
        {
            var menu = new Menu() { Adi = model.Adi };
            menu.Status = Status.Modified;
            menu.GuncelleZamani = DateTime.Now;
            return _menuREPO.Update(menu);
        }

        public async Task<int> UpdateStatus(int id)
        {
            var menu = await _menuREPO.GetByIdAsync(id);
            if (menu.Status == Status.Added)
                menu.Status = Status.Deleted;
            else
                menu.Status = Status.Added;

            return _menuREPO.Update(menu);
        }
    }
}
