using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.SosSiparisService
{
    public interface ISosSiparisService
    {
        Task<List<SosSiparis>> GetAllSosSiparis();
        Task<List<SosSiparis>> GetAllSosSiparisActive();
        Task<SosSiparis> GetSosSiparis(int id);
        Task<List<SosSiparis>> GetAllSosSiparisDeleted();
        Task<int> DeletedStatus(int id);
        Task<int> Delete(int id);
    }
}
