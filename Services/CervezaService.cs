using CerveceriaAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CerveceriaAPI.Services
{
    public class CervezaService : ICervezaService
    {
        private readonly string _conStr;

        public CervezaService(IConfiguration configuration)
        {
            _conStr = configuration.GetConnectionString("CerveceriaDB") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IEnumerable<Cerveza>?> GetCerveza(string nombre = "", int idMarca = 0)
        {
            List<Cerveza> cervezas = new List<Cerveza>();

            try
            {
                using (var connection = new SqlConnection(_conStr))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("GetCerveza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 20).Value = nombre;
                        command.Parameters.Add("@id_Marca", SqlDbType.Int).Value = idMarca == 0 ? DBNull.Value : (object)idMarca;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Cerveza cerveza = new Cerveza
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id_Cerveza")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                                };

                                Marca marca = new Marca
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id_Marca")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Marca"))
                                };

                                cerveza.Marca = marca;
                                cervezas.Add(cerveza);
                            }
                        }
                    }
                }

                return cervezas;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> SaveCerveza(Cerveza cerveza)
        {
            try
            {
                using (var connection = new SqlConnection(_conStr))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("InsertCerveza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 20).Value = cerveza.Nombre;
                        command.Parameters.Add("@id_Marca", SqlDbType.Int).Value = cerveza.Marca?.Id ?? (object)DBNull.Value;

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

        public async Task<bool> UpdateCerveza(Cerveza cerveza)
        {
            try
            {
                using (var connection = new SqlConnection(_conStr))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("UpdateCerveza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id_Cerveza", SqlDbType.Int).Value = cerveza.Id;
                        command.Parameters.Add("@Nombre", SqlDbType.VarChar, 20).Value = cerveza.Nombre;
                        command.Parameters.Add("@id_Marca", SqlDbType.Int).Value = cerveza.Marca?.Id ?? (object)DBNull.Value;

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

        public async Task<bool> DeleteCerveza(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_conStr))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("DeleteCerveza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@id_Cerveza", SqlDbType.Int).Value = id;

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

    public interface ICervezaService
    {
        Task<IEnumerable<Cerveza>?> GetCerveza(string nombre, int idMarca);
        Task<bool> SaveCerveza(Cerveza cerveza);
        Task<bool> UpdateCerveza(Cerveza cerveza);
        Task<bool> DeleteCerveza(int id);
    }
}
