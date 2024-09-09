using Introduction.Repository.Common;
using Npgsql;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;

namespace Introduction.Repository
{
    public class FishRepository : IFishRepository
    {
        private static string connectionString = "Host=localhost;Port=5432;Database=Aquarium;Username=postgres;Password=00000";

        public async Task<Fish> GetFishAsync(Guid id)
        {
            try
            {
                Fish fish = new Fish();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Fish\" WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

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
                return fish;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Fish>> GetAllFishesAsync()
        {
            try
            {
                List<Fish> fishes = new List<Fish>();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Fish\";";
                using var command = new NpgsqlCommand(commandText, connection);

                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

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
                return fishes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<bool> PostFishAsync(Fish fish)
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
                int numberOfCommits = await command.ExecuteNonQueryAsync();
                connection.Close();

                return numberOfCommits != 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteFishAsync(string name)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "DELETE FROM \"Fish\" WHERE \"Name\" = @name;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@name", name);

                connection.Open();
                int numberOfCommits = await command.ExecuteNonQueryAsync();
                connection.Close();

                return numberOfCommits != 0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteFishAsync(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "DELETE FROM \"Fish\" WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                int numberOfCommits = await command.ExecuteNonQueryAsync();
                connection.Close();

                return numberOfCommits != 0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DomesticateFishAsync(string name)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "UPDATE \"Fish\" SET \"IsAggressive\" = false WHERE \"Name\" = @name;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@name", name);

                connection.Open();
                int numberOfCommits = await command.ExecuteNonQueryAsync();
                connection.Close();

                return numberOfCommits != 0;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
