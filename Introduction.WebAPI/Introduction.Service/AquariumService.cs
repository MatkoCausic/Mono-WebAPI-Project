using Introduction.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service
{
    public class AquariumService
    {
        public async Task<List<Aquarium>> GetAllAquariumsAsync()
        {
            AquariumRepository repository = new();
            return await repository.GetAllAquariumsAsync();
        }
        public async Task<Aquarium> GetAquariumAsync(Guid id)
        {
            AquariumRepository repository = new();
            return await repository.GetAquariumAsync(id);
        }
        public async Task<bool> PostAquariumAsync(Aquarium aquarium)
        {
            AquariumRepository repository = new();
            return await repository.PostAquariumAsync(aquarium);
        }
        public async Task<bool> DeleteAquariumAsync(Guid id)
        {
            AquariumRepository repository = new();
            return await repository.DeleteAquariumAsync(id);
        }
        public async Task<bool> ChangeOwner(Guid id,string newOwner)
        {
            AquariumRepository repository = new();
            return await repository.ChangeOwner(id, newOwner);
        }
    }
}
