using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service.Common
{
    public interface IFishService
    {
        List<Fish> GetAllFishes();
        bool PostFish(Fish fish);
        bool DeleteFish(string name);
        bool DeleteFish(Guid id);
        Fish GetFish(Guid id);
    }
}
