using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.MenuService
{
    public interface IMenuService
    {
        int Create(MenuCreateDTO model);
        int Update(MenuCreateDTO model);
        Task<int> UpdateStatus(int id);
        Task<int> Delete(int id);
        Task<List<Menu>> GetAllMenu();
        Task<List<Menu>> GetAllMenuActive();
        Task<Menu> GetMenu(int id);
    }
}
