using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.BurgerService
{
    public interface IHamburgerService
    {
        int Create(BurgerCreateDTO model);
        int Update(BurgerCreateDTO model);
        Task<int> UpdateStatus(int id);
        Task<int> Delete(int id);
        Task<List<Hamburger>> GetAllBurger();
        Task<List<Hamburger>> GetAllBurgerActive();
        Task<Hamburger> GetBurger(int id);



    }
}
