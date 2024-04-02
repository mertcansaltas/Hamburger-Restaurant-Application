using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.SosService
{
    public interface ISosService
    {
        int Create(SosCreateDTO model);
        int Update(SosCreateDTO model);
        Task<int> UpdateStatus(int id);
        Task<int> Delete(int id);
        Task<List<Sos>> GetAllSos();
        Task<List<Sos>> GetAllSosActive();
        Task<Sos> GetSos(int id);

    }
}
