using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Repository
{
    public class AquariumRepository
    {
        private static string connectionString = "Host=localhost;Port=5432;Database=Aquarium;Username=postgres;Password=00000";

        public async Task<List<Aquarium>> GetAllAquariumsAsync()
        {
            try
            {
                List<Aquarium> aquariums = new();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Aquarium\";";
                using var command = new NpgsqlCommand(commandText, connection);

                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    Aquarium aquarium = new()
                    {
                        Id = Guid.Parse(reader[0].ToString()),
                        OwnerName = reader[1].ToString(),
                        Shape = reader[2].ToString(),
                        IsHandmade = Convert.ToBoolean(reader[3]),
                        Volume = Convert.ToDouble(reader[4]),
                        Fishes = new List<Fish>()
                    };

                    aquariums.Add(aquarium);
                }

                connection.Close();
                return aquariums;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<Aquarium> GetAquariumAsync(Guid id)
        {
            try
            {
                Aquarium aquarium = new();
                using var connection = new NpgsqlConnection(connectionString);
                var commandTextFishes = "SELECT \"Fish\".\"Id\", \"Name\",\"Color\",\"IsAggressive\",\"AquariumId\"" +
                                    "FROM \"Fish\"" +
                                    "RIGHT JOIN \"Aquarium\"" +
                                    "ON \"Fish\".\"AquariumId\" = @id;";
                using var commandFishes = new NpgsqlCommand(commandTextFishes, connection);

                commandFishes.Parameters.AddWithValue("@id", id);

                connection.Open();
                using NpgsqlDataReader readerFishes = await commandFishes.ExecuteReaderAsync();
                while (readerFishes.Read())
                {
                    Fish fish = new()
                    {
                        Id = Guid.Parse(readerFishes[0].ToString()),
                        Name = readerFishes[1].ToString(),
                        Color = readerFishes[2].ToString(),
                        IsAggressive = Convert.ToBoolean(readerFishes[3]),
                        AquariumId = Guid.TryParse(readerFishes[4].ToString(), out var result) ? result : null
                    };
                    aquarium.Fishes.Add(fish);
                }
                
                connection.Close();

                var commandTextAquarium = "SELECT * FROM \"Aquarium\" WHERE \"Id\" = @id;";
                using var commandAquarium = new NpgsqlCommand(commandTextAquarium, connection);

                commandAquarium.Parameters.AddWithValue("@id", id);

                connection.Open();
                using NpgsqlDataReader readerAquarium = await commandAquarium.ExecuteReaderAsync();
                if (readerAquarium.HasRows)
                {
                    readerAquarium.Read();

                    aquarium.Id = Guid.Parse(readerAquarium[0].ToString());
                    aquarium.OwnerName = readerAquarium[1].ToString();
                    aquarium.Shape = readerAquarium[2].ToString();
                    aquarium.IsHandmade = Convert.ToBoolean(readerAquarium[3]);
                    aquarium.Volume = Convert.ToDouble(readerAquarium[4]);
                }
                connection.Close();
                return aquarium;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> PostAquariumAsync(Aquarium aquarium)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                string commandText = $"INSERT INTO \"Aquarium\" (\"Id\",\"OwnerName\",\"Shape\",\"IsHandmade\",\"Volume\") VALUES (@id,@ownerName,@shape,@isHandmade,@volume);";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
                command.Parameters.AddWithValue("@name", aquarium.OwnerName);
                command.Parameters.AddWithValue("@shape", aquarium.Shape);
                command.Parameters.AddWithValue("@isHandmade", aquarium.IsHandmade);
                command.Parameters.AddWithValue("@volume", aquarium.Volume);
                aquarium.Fishes = new List<Fish>();

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
        public async Task<bool> DeleteAquariumAsync(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "DELETE FROM \"Aquarium\" WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id",id);

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
        public async Task<bool> ChangeOwner(Guid id,string newOwner)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "UPDATE \"Aquarium\" SET \"OwnerName\" = @newOwner WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@newOwner", newOwner);
                command.Parameters.AddWithValue("@id", id);

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
