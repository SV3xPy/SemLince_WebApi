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
    public class CareerRepository : ICareerRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public CareerRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Career> AddAsync(Career entity)
        {
            using SqlConnection connection = _connectionFactory.CreateConnection();
            using SqlCommand command = new("SP_SemLince_Carrera", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 3, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@NameCareer", SqlDbType.NVarChar) { Value = entity.Car_Nombre, Direction = ParameterDirection.Input });
            await connection.OpenAsync();

            int newId = Convert.ToInt32(await command.ExecuteScalarAsync());

            if (newId == 0)
            {
                throw new Exception("Error al insertar.");
            }
            return new Career
            {
                Car_ID = newId,
                Car_Nombre = entity.Car_Nombre
            };
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using SqlConnection connection = _connectionFactory.CreateConnection();
            using SqlCommand command = new("SP_SemLince_Carrera", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 4, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@IdCareer", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input });
            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Career>> GetAllAsync()
        {
            List<Career> result = [];
            using SqlConnection connection = _connectionFactory.CreateConnection();
            using SqlCommand command = new("SP_SemLince_Carrera", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 1, Direction = ParameterDirection.Input });
            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(new Career
                {
                    Car_ID = Convert.ToInt32(reader["Car_ID"]),
                    Car_Nombre = reader["Car_Nombre"].ToString()
                });
            }
            return result;
        }

        public async Task<Career> GetByIdAsync(int id)
        {
            using SqlConnection connection = _connectionFactory.CreateConnection();
            using SqlCommand command = new("SP_SemLince_Carrera", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 2, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@IdCareer", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input });
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                return new Career
                {
                    Car_ID = Convert.ToInt32(reader["Car_ID"]),
                    Car_Nombre = reader["Car_Nombre"].ToString()
                };
            }
            return null;
        }

        public async Task<Career> UpdateAsync(int id,Career entity)
        {
            using SqlConnection connection = _connectionFactory.CreateConnection();
            using SqlCommand command = new("SP_SemLince_Carrera", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 5, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@IdCareer", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@NameCareer", SqlDbType.NVarChar) { Value = entity.Car_Nombre, Direction = ParameterDirection.Input });
            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected <= 0)
            {
                return null;
            }
            return new Career
            {
                Car_ID = id,
                Car_Nombre = entity.Car_Nombre
            };
        }
    }
}
