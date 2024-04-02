using BurgerService.DTOs;
using MVCPROJE.ENTITIES.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerService.Services.IcecekService
{
    public interface IIcecekService
    {
        int Create(IcecekCreateDTO model);
        int Update(IcecekCreateDTO model);
        Task<int> UpdateStatus(int id);
        Task<int> Delete(int id);
        Task<List<Icecek>> GetAllIcecek();
        Task<List<Icecek>> GetAllIcecekActive();
        Task<Icecek> GetIcecek(int id);
       
    }
}
