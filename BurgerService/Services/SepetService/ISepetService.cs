using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.SepetService
{
    public interface ISepetService
    {
        int Create(SepetCreateDTO model);
        int Update(SepetCreateDTO model);
        Task<int> UpdateStatus(int id);
        Task<int> Delete(int id);
        Task<List<Sepet>> GetAllSepet();
        Task<List<Sepet>> GetAllSepetActive();

        Task<Sepet> GetSepet(int id);
    }
}
