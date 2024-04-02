using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.IcecekSiparisService
{
    public interface IIcecekSiparisService
    {
        Task<List<IcecekSiparis>> GetAllIcecekSiparis();
        Task<List<IcecekSiparis>> GetAllIcecekSiparisActive();
        Task<IcecekSiparis> GetIcecekSiparis(int id);
        Task<int> Delete(int id);
        Task<List<IcecekSiparis>> GetAllIcecekDeleted();
        Task<int> DeletedStatus(int id);
    }
}
