using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SemLince_Application.IRepositories;
using SemLince_Domain.Entities;

namespace SemLince_Infrastructure.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public BuildingRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Building> AddAsync(Building entity)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Edificio", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Accion", SqlDbType.Int) { Value = 3, Direction = ParameterDirection.Input },
                    new SqlParameter("@NameBuilding", SqlDbType.NVarChar) { Value = entity.Edi_Nombre, Direction = ParameterDirection.Input },
                    new SqlParameter("@CampusBuilding", SqlDbType.Int) { Value = entity.Edi_Campus, Direction = ParameterDirection.Input }
                }
            };
            await connection.OpenAsync();

            int newId = Convert.ToInt32(await command.ExecuteScalarAsync());

            if (newId == 0)
            {
                throw new Exception("Error al insertar.");
            }
            return new Building
            {
                Edi_ID = newId,
                Edi_Nombre = entity.Edi_Nombre,
                Edi_Campus = entity.Edi_Campus
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Edificio", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Accion", SqlDbType.Int) { Value = 4, Direction = ParameterDirection.Input },
                    new SqlParameter("@IdBuilding", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input }
                }
            };
            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Building>> GetAllAsync()
        {
            List<Building> result = [];
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Edificio", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Accion", SqlDbType.Int) { Value = 1, Direction = ParameterDirection.Input }
                }
            };
            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(new Building
                {
                    Edi_ID = reader.GetInt32(0),
                    Edi_Nombre = reader["Edi_Nombre"].ToString() ?? "",
                    Edi_Campus = Convert.ToInt32(reader["Edi_Campus"])
                });
            }
            return result;
        }

        public async Task<Building> GetByIdAsync(int id)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Edificio", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Accion", SqlDbType.Int) { Value = 2, Direction = ParameterDirection.Input },
                    new SqlParameter("@IdBuilding", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input }

                }
            };
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                return new Building
                {
                    Edi_ID = id,
                    Edi_Nombre = reader["Edi_Nombre"].ToString() ?? "",
                    Edi_Campus = Convert.ToInt32(reader["Edi_Campus"])
                };
            }
            return null;
        }

        public async Task<Building> UpdateAsync(int id, Building entity)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Edificio", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Accion", SqlDbType.Int) { Value = 5, Direction = ParameterDirection.Input },
                    new SqlParameter("@IdBuilding", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input },
                    new SqlParameter("@NameBuilding", SqlDbType.NVarChar) { Value = entity.Edi_Nombre, Direction = ParameterDirection.Input },
                    new SqlParameter("@CampusBuilding", SqlDbType.NVarChar) { Value = entity.Edi_Campus, Direction = ParameterDirection.Input }
                }
            };
            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected <= 0)
            {
                return null;
            }
            return new Building
            {
                Edi_ID = id,
                Edi_Nombre = entity.Edi_Nombre,
                Edi_Campus = entity.Edi_Campus
            };
        }
    }
}
