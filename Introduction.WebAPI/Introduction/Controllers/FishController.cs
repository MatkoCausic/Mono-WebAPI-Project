using Microsoft.AspNetCore.Mvc;
using Npgsql;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;

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
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "DELETE FROM \"Fish\" WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                int numberOfCommits = command.ExecuteNonQuery();
                connection.Close();

                if (numberOfCommits == 0)
                    return NotFound("Fish with given id doesn't exist.");
                return Ok("Deleted!");

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("remove/name/{name}")]
        public IActionResult DeleteFish(string name)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "DELETE FROM \"Fish\" WHERE \"Name\" = @name;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@name", name);

                connection.Open();
                int numberOfCommits = command.ExecuteNonQuery();
                connection.Close();

                if (numberOfCommits == 0)
                    return NotFound("Fish with given name doesn't exist.");
                return Ok("Deleted!");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("add")]
        public IActionResult PostFish([FromBody] Fish fish)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                string commandText = $"INSERT INTO \"Fish\" (\"Id\",\"Name\",\"Color\",\"IsAggressive\",\"AquariumId\") VALUES (@Id,@Name,@Color,@IsAggressive,@AquariumId);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@Name", fish.Name);
                command.Parameters.AddWithValue("@Color", fish.Color);
                command.Parameters.AddWithValue("@IsAggressive", fish.IsAggressive);
                command.Parameters.AddWithValue("@AquariumId", NpgsqlTypes.NpgsqlDbType.Uuid, fish.AquariumId ?? (object)DBNull.Value);

                connection.Open();
                int numberOfCommits = command.ExecuteNonQuery();
                connection.Close();

                if (numberOfCommits == 0)
                    return BadRequest("Unable to add fish to the table.");
                return Ok("Successfully added.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get/all")]
        public IActionResult GetAllFishes()
        {
            try
            {
                List<Fish> fishes = new List<Fish>();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Fish\";";
                using var command = new NpgsqlCommand(commandText, connection);

                connection.Open();
                using NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Fish fish = new Fish()
                    {
                        Id = Guid.Parse(reader[0].ToString()),
                        Name = reader[1].ToString(),
                        Color = reader[2].ToString(),
                        IsAggressive = Convert.ToBoolean(reader[3]),
                        AquariumId = Guid.TryParse(reader[4].ToString(), out var result) ? result : null
                    };

                    fishes.Add(fish);
                }
                connection.Close();
                if (fishes == null)
                    return NotFound("There are no entities in table.");
                return Ok(fishes);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        public IActionResult GetFish(Guid id)
        {
            try
            {
                Fish fish = new Fish();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Fish\" WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    fish.Id = Guid.Parse(reader[0].ToString());
                    fish.Name = reader[1].ToString();
                    fish.Color = reader[2].ToString();
                    fish.IsAggressive = Convert.ToBoolean(reader[3]);
                    fish.AquariumId = Guid.TryParse(reader[4].ToString(), out var result) ? result : null;
                }
                connection.Close();
                if (fish == null)
                    return NotFound("Fish with that id does not exit.");
                return Ok(fish);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
