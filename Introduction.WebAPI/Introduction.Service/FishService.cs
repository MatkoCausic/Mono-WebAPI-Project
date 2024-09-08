using Introduction.Repository;
using Introduction.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service
{
    public class FishService : IFishService
    {
        public List<Fish> GetAllFishes()
        {
            FishRepository repository = new();
            return repository.GetAllFishes();
        }
        public bool PostFish(Fish fish)
        {
            FishRepository repository = new();
            return repository.PostFish(fish);
        }
        public bool DeleteFish(string name)
        {
            FishRepository repository= new();
            return repository.DeleteFish(name);
        }

        public bool DeleteFish(Guid id)
        {
            FishRepository repository= new();
            return repository.DeleteFish(id);
        }
        public Fish GetFish(Guid id)
        {
            FishRepository repository = new();
            return repository.GetFish(id);
        }
    }
}
