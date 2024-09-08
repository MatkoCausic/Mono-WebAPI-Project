using Introduction.Repository.Common;
using Npgsql;
using NpgsqlTypes;
using System.ComponentModel.DataAnnotations;

namespace Introduction.Repository
{
    public class FishRepository : IFishRepository
    {
        private static string connectionString = "Host=localhost;Port=5432;Database=Aquarium;Username=postgres;Password=00000";

        public List<Fish> GetAllFishes()
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
                return fishes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public bool PostFish(Fish fish)
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

                return numberOfCommits != 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteFish(string name)
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

                return numberOfCommits != 0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteFish(Guid id)
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

                return numberOfCommits != 0;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Fish GetFish(Guid id)
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
                return fish;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
