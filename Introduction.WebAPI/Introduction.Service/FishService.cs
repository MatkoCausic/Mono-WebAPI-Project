using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service
{
    public class FishService : IFishService
    {
        private IFishRepository repository = new FishRepository();


        public async Task<List<Fish>> GetAllFishesAsync()
        {
            FishRepository repository = new();
            return await repository.GetAllFishesAsync();
        }
        public async Task<bool> PostFishAsync(Fish fish)
        {
            FishRepository repository = new();
            return await repository.PostFishAsync(fish);
        }
        public async Task<bool> DeleteFishAsync(string name)
        {
            FishRepository repository= new();
            return await repository.DeleteFishAsync(name);
        }

        public async Task<bool> DeleteFishAsync(Guid id)
        {
            FishRepository repository = new();
            return await repository.DeleteFishAsync(id);
        }
        public async Task<bool> DomesticateFishAsync(string name)
        {
            FishRepository repository = new();
            return await repository.DomesticateFishAsync(name);
        }
        public async Task<Fish> GetFishAsync(Guid id)
        {
            FishRepository repository = new();
            return await repository.GetFishAsync(id);
        }
        public async Task<bool> UpdateFishAsync(Guid id,Fish fish)
        {
            var currentFish = await repository.GetFishAsync(id);
            return (currentFish == null) ? false : await this.repository.UpdateFishAsync(id, fish);
        }
    }
}
