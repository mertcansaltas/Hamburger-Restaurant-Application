using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.MenuSiparisService
{
    public interface IMenuSiparisService
    {
        Task<List<MenuSiparis>> GetAllMenuSiparis();
        Task<List<MenuSiparis>> GetAllMenuSiparisActive();
        Task<MenuSiparis> GetMenuSiparis(int id);
        Task<List<MenuSiparis>> GetAllMenuSiparisDeleted();
        Task<int> DeletedStatus(int id);
        Task<int> Delete(int id);
    }
}
