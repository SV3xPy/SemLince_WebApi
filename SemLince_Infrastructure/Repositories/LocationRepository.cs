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
    public class LocationRepository : ILocationRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;
        public LocationRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Location> AddAsync(Location entity)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Ubicacion", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters = {
                    new SqlParameter("@Accion", SqlDbType.Int) { Value = 3, Direction = ParameterDirection.Input },
                    new SqlParameter("@NameLocation", SqlDbType.NVarChar) { Value = entity.Loc_Name, Direction = ParameterDirection.Input },
                    new SqlParameter("@LatitudLocation", SqlDbType.Decimal) { Value = entity.Loc_Latitud, Direction = ParameterDirection.Input },
                    new SqlParameter("@LongitudLocation", SqlDbType.Decimal) { Value = entity.Loc_Longitud, Direction = ParameterDirection.Input },
                    new SqlParameter("@CapacityLocation", SqlDbType.SmallInt) { Value = entity.Loc_Capacity, Direction = ParameterDirection.Input },
                    new SqlParameter("@BuildingLocation", SqlDbType.Int) { Value = entity.Loc_Building, Direction = ParameterDirection.Input }
                }
            };
            await connection.OpenAsync();

            int newId = Convert.ToInt32(await command.ExecuteScalarAsync());

            if (newId == 0)
            {
                throw new Exception("Error al insertar.");
            }
            return new Location
            {
                Loc_ID = newId,
                Loc_Name = entity.Loc_Name,
                Loc_Latitud = entity.Loc_Latitud,
                Loc_Longitud = entity.Loc_Longitud,
                Loc_Capacity = entity.Loc_Capacity,
                Loc_Building = entity.Loc_Building,
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Ubicacion", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Accion", SqlDbType.Int) { Value = 4, Direction = ParameterDirection.Input },
                    new SqlParameter("@IdLocation", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input }
                }
            };
            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            List<Location> result = [];
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Ubicacion", connection)
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
                result.Add(new Location
                {
                    Loc_ID = reader.GetInt32(0),
                    Loc_Name = reader["Ubi_Nombre"].ToString() ?? "",
                    Loc_Longitud = Convert.ToDouble(reader["Ubi_Longitud"]),
                    Loc_Latitud = Convert.ToDouble(reader["Ubi_Latitud"]),
                    Loc_Capacity = Convert.ToInt32(reader["Ubi_Capacidad"]),
                    Loc_Building = reader["Ubi_Edificio"] is DBNull ? 0 : Convert.ToInt32(reader["Ubi_Edificio"])
                });
            }
            return result;
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Ubicacion", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Accion", SqlDbType.Int) { Value = 2, Direction = ParameterDirection.Input },
                    new SqlParameter("@IdLocation", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input }

                }
            };
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                return new Location
                {
                    Loc_ID = id,
                    Loc_Name = reader["Ubi_Nombre"].ToString() ?? "",
                    Loc_Longitud = Convert.ToDouble(reader["Ubi_Longitud"]),
                    Loc_Latitud = Convert.ToDouble(reader["Ubi_Latitud"]),
                    Loc_Capacity = Convert.ToInt32(reader["Ubi_Capacidad"]),
                    Loc_Building = reader["Ubi_Edificio"] is DBNull ? 0 : Convert.ToInt32(reader["Ubi_Edificio"])
                };
            }
            return null;
        }

        public async Task<Location> UpdateAsync(int id, Location entity)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Ubicacion", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters = {
                    new SqlParameter("@Accion", SqlDbType.Int) { Value = 5, Direction = ParameterDirection.Input },
                    new SqlParameter("@IdLocation", SqlDbType.Int) {Value = id, Direction =ParameterDirection.Input},
                    new SqlParameter("@NameLocation", SqlDbType.NVarChar) { Value = entity.Loc_Name, Direction = ParameterDirection.Input },
                    new SqlParameter("@LatitudLocation", SqlDbType.Decimal) { Value = entity.Loc_Latitud, Direction = ParameterDirection.Input },
                    new SqlParameter("@LongitudLocation", SqlDbType.Decimal) { Value = entity.Loc_Longitud, Direction = ParameterDirection.Input },
                    new SqlParameter("@CapacityLocation", SqlDbType.SmallInt) { Value = entity.Loc_Capacity, Direction = ParameterDirection.Input },
                    new SqlParameter("@BuildingLocation", SqlDbType.Int) { Value = entity.Loc_Building, Direction = ParameterDirection.Input }
                }
            };
            await connection.OpenAsync();
            int rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected <= 0)
            {
                return null;
            }
            return new Location
            {
                Loc_ID = id,
                Loc_Name = entity.Loc_Name,
                Loc_Longitud = entity.Loc_Longitud,
                Loc_Latitud = entity.Loc_Latitud,
                Loc_Building = entity.Loc_Building,
                Loc_Capacity = entity.Loc_Capacity
            };
        }
    }
}
