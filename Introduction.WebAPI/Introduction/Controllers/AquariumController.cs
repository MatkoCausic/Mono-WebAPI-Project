using Introduction.Service;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.WebAPI.Controllers
{
    [ApiController]
    [Route("aquariums")]
    public class AquariumController : Controller
    {
        [HttpGet]
        [Route("get/all")]
        public async Task<IActionResult> GetAllAquariumsAsync()
        {
            AquariumService service = new();
            var isSuccessful = await service.GetAllAquariumsAsync();
            if (isSuccessful == null)
                return NotFound("There are no entities in table.");
            return Ok(isSuccessful);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetAquariumAsync(Guid id)
        {
            AquariumService service = new();
            var isSuccessful = await service.GetAquariumAsync(id);
            if (isSuccessful == null)
                return NotFound("There is no such entity.");
            return Ok(isSuccessful);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> PostAquariumAsync([FromBody] Aquarium aquarium)
        {
            AquariumService service = new();
            bool isSuccessful = await service.PostAquariumAsync(aquarium);
            if (!isSuccessful)
                return BadRequest("Unable to add aquarium to the database.");
            return Ok("Successfully added!");
        }

        [HttpDelete]
        [Route("remove/{id}")]
        public async Task<IActionResult> DeleteAquariumAsync(Guid id)
        {
            AquariumService service = new();
            bool isSuccessful = await service.DeleteAquariumAsync(id);
            if (!isSuccessful)
                return BadRequest("Unable to find/delete aquarium with given id.");
            return Ok("Successfully deleted!");
        }

        [HttpPut]
        [Route("changeOwner/{id}/{name}")]
        public async Task<IActionResult> ChangeOwner(Guid id,string newOwner)
        {
            AquariumService service = new();
            var isSuccessful = await service.ChangeOwner(id, newOwner);
            if (isSuccessful == null)
                return NotFound("There is no entity wish such id.");
            return Ok("Aquarium successfully changed owner!");
        }
    }
}
