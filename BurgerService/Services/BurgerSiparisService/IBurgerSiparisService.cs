using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.BurgerSiparisService
{
    public interface IBurgerSiparisService
    {
      
        
        Task<List<BurgerSiparis>> GetAllBurgerSiparis();
        Task<List<BurgerSiparis>> GetAllBurgerSiparisActive();
        Task<BurgerSiparis> GetBurgerSiparis(int id);
        Task<List<BurgerSiparis>> GetAllBurgerSiparisDeleted();
        Task<int> DeletedStatus(int id);
        Task<int> Delete(int id);
    }
}
