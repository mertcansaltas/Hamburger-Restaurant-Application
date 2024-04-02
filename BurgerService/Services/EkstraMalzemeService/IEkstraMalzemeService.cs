using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.EkstraMalzemeService
{
    public interface IEkstraMalzemeService
    {
        int Create(EkstraMalzemeCreateDTO model);
        int Update(EkstraMalzemeCreateDTO model);
        Task<int> UpdateStatus(int id);
        Task<int> Delete(int id);
        Task<List<EkstraMalzeme>> GetAllEkstraMalzeme();
        Task<List<EkstraMalzeme>> GetAllEkstraMalzemeActive();
        Task<EkstraMalzeme> GetEkstraMalzeme(int id);
    }
}
