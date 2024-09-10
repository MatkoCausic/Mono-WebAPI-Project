using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service.Common
{
    public interface IFishService
    {
        Task<List<Fish>> GetAllFishesAsync();
        Task<bool> PostFishAsync(Fish fish);
        Task<bool> DeleteFishAsync(string name);
        Task<bool> DeleteFishAsync(Guid id);
        Task<Fish> GetFishAsync(Guid id);
        Task<bool> DomesticateFishAsync(string name);
    }
}
