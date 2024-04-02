using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.EkstraMalzemeSiparisService
{
    public interface IEkstraMalzemeSiparisService
    {
        Task<List<EkstraMalzemeSiparis>> GetAllEkstraMalzemeSiparis();
        Task<List<EkstraMalzemeSiparis>> GetAllEkstraMalzemeSiparisActive();
        Task<EkstraMalzemeSiparis> GetEkstraMalzemeSiparis(int id);
        Task<List<EkstraMalzemeSiparis>> GetAllEkstraMalzemeDeleted();
        Task<int> DeletedStatus(int id);
        Task<int> Delete(int id);
    }
}
