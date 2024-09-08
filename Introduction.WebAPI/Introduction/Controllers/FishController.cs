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
        static string connectionString = "Host=localhost;Port=5432;Database=Aquarium;Username=postgres;Password=00000";

        [HttpDelete]
        [Route("remove/id/{id}")]
        public IActionResult DeleteFish(Guid id)
        {
            FishService service = new();
            bool isSuccessful = service.DeleteFish(id);
            if (!isSuccessful)
                return BadRequest("Unable to find/delete fish with given id.");
            return Ok("Successfully deleted!");
        }

        [HttpDelete]
        [Route("remove/name/{name}")]
        public IActionResult DeleteFish(string name)
        {
            FishService service = new();
            bool isSuccessful = service.DeleteFish(name);
            if (!isSuccessful)
                return BadRequest("Unable to find/delete fish with given name.");
            return Ok("Successfully deleted!");
        }

        [HttpPost]
        [Route("add")]
        public IActionResult PostFish([FromBody] Fish fish)
        {
            FishService service = new();
            bool isSuccessful = service.PostFish(fish);
            if (!isSuccessful)
                return BadRequest("Unable to add fish to the database.");
            return Ok("Successfully added!");
        }

        [HttpGet]
        [Route("get/all")]
        public IActionResult GetAllFishes()
        {
            FishService service = new();
            var isSuccessful = service.GetAllFishes();
            if(isSuccessful == null)
                return NotFound("There are no entities in table.");
            return Ok(isSuccessful);
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetFish(Guid id)
        {
            FishService service = new();
            var isSuccessful = service.GetFish(id);
            if (isSuccessful == null)
                return NotFound("There is no such entity.");
            return Ok(isSuccessful);
        }
    }
}
