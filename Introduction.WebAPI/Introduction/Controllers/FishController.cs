using Introduction.Service;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Introduction.Controllers
{
    [ApiController]
    [Route("fishes")]
    public class FishController : Controller
    {
        [HttpDelete]
        [Route("remove/{id}")]
        public async Task<IActionResult> DeleteFishAsync(Guid id)
        {
            FishService service = new();
            bool isSuccessful = await service.DeleteFishAsync(id);
            if (!isSuccessful)
                return BadRequest("Unable to find/delete fish with given id.");
            return Ok("Successfully deleted!");
        }

        [HttpDelete]
        [Route("remove/name/{name}")]
        public async Task<IActionResult> DeleteFishAsync(string name)
        {
            FishService service = new();
            bool isSuccessful = await service.DeleteFishAsync(name);
            if (!isSuccessful)
                return BadRequest("Unable to find/delete fish with given name.");
            return Ok("Successfully deleted!");
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> PostFishAsync([FromBody] Fish fish)
        {
            FishService service = new();
            bool isSuccessful = await service.PostFishAsync(fish);
            if (!isSuccessful)
                return BadRequest("Unable to add fish to the database.");
            return Ok("Successfully added!");
        }

        [HttpGet]
        [Route("get/all")]
        public async Task<IActionResult> GetAllFishesAsync()
        {
            FishService service = new();
            var isSuccessful = await service.GetAllFishesAsync();
            if(isSuccessful == null)
                return NotFound("There are no entities in table.");
            return Ok(isSuccessful);
        }

        [HttpPut]
        [Route("domesticate/name/{name}")]
        public async Task<IActionResult> DomesticateFishAsync(string name)
        {
            FishService service = new();
            var isSuccessul = await service.DomesticateFishAsync(name);
            if (isSuccessul == null)
                return NotFound("There are no entities with such name.");
            return Ok("Fish successfully domesticated!");
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetFishAsync(Guid id)
        {
            FishService service = new();
            var isSuccessful = await service.GetFishAsync(id);
            if (isSuccessful == null)
                return NotFound("There is no such entity.");
            return Ok(isSuccessful);
        }
    }
}
