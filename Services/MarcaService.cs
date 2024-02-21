using CerveceriaAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CerveceriaAPI.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly string _conStr;

        public MarcaService(IConfiguration configuration)
        {
            _conStr = configuration.GetConnectionString("CerveceriaDB") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IEnumerable<Marca>?> GetMarca()
        {
            List<Marca> marcas = new List<Marca>();

            try
            {
                using (var connection = new SqlConnection(_conStr))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("GetMarca", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                marcas.Add(new Marca
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id_Marca")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Marca"))
                                });
                            }
                        }
                    }
                }

                return marcas;
            }
            catch (SqlException ex)
            {
                // Log SQL-related exceptions
                Console.WriteLine("SQL Exception occurred:");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Error Number: {ex.Number}");
                Console.WriteLine($"Severity: {ex.Class}");
                Console.WriteLine($"State: {ex.State}");
                // Log additional details if needed

                // Rethrow the exception to see the full stack trace
                throw;
            }
            catch (Exception ex)
            {
                // Catch other types of exceptions
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return null;
            }
        }


        public async Task<bool> SaveMarca(Marca marca)
        {
            try
            {
                using (var connection = new SqlConnection(_conStr))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("InsertMarca", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Marca", SqlDbType.VarChar, 20).Value = marca.Nombre;

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateMarca(Marca marca)
        {
            try
            {
                using (var connection = new SqlConnection(_conStr))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("UpdateMarca", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id_Marca", SqlDbType.Int).Value = marca.Id;
                        command.Parameters.Add("@Marca", SqlDbType.VarChar, 20).Value = marca.Nombre;

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteMarca(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_conStr))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("DeleteMarca", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id_Marca", SqlDbType.Int).Value = id;

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }

    public interface IMarcaService
    {
        Task<IEnumerable<Marca>?> GetMarca();
        Task<bool> SaveMarca(Marca marca);
        Task<bool> UpdateMarca(Marca marca);
        Task<bool> DeleteMarca(int id);
    }

}
