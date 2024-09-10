using Introduction.Repository;
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
    }
}
