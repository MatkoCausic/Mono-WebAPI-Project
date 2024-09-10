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
                        fishes = new List<Fish>()
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
                Aquarium aquarium = new Aquarium();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Aquarium\" WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    reader.Read();

                    aquarium.Id = Guid.Parse(reader[0].ToString());
                    aquarium.OwnerName = reader[1].ToString();
                    aquarium.Shape = reader[2].ToString();
                    aquarium.IsHandmade = Convert.ToBoolean(reader[3]);
                    aquarium.Volume = Convert.ToDouble(reader[4]);
                    aquarium.fishes = new List<Fish>();
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
                aquarium.fishes = new List<Fish>();

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
