using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging.Abstractions;
using SemLince_Application.IRepositories;
using SemLince_Domain.Entities;

namespace SemLince_Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;
        public EventRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Event> AddAsync(Event entity)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Evento", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Accion", SqlDbType.Int){Value = 3, Direction = ParameterDirection.Input},
                    new SqlParameter("@DescriptionEvent", SqlDbType.VarChar){Value = entity.Eve_Description, Direction = ParameterDirection.Input},
                    new SqlParameter("@DateHourStartEvent", SqlDbType.SmallDateTime){Value = entity.Eve_DateHourStart, Direction = ParameterDirection.Input},
                    new SqlParameter("@DurationEvent", SqlDbType.Int){Value=entity.Eve_Duration, Direction = ParameterDirection.Input},
                    new SqlParameter("@LocationEvent", SqlDbType.Int){Value=entity.Eve_IdLocation, Direction = ParameterDirection.Input},
                    new SqlParameter("@CareerEvent", SqlDbType.Int){Value=entity.Eve_IdCareer, Direction = ParameterDirection.Input},
                    new SqlParameter("@CategoryEvent", SqlDbType.Int){Value=entity.Eve_IdCategory, Direction= ParameterDirection.Input}
                }
            };
            await connection.OpenAsync();

            int newId = Convert.ToInt32(await command.ExecuteScalarAsync());

            if (newId == 0)
            {
                throw new Exception("Error al insertar.");
            }
            return new Event
            {
                Eve_ID = newId,
                Eve_Description = entity.Eve_Description,
                Eve_DateHourStart = entity.Eve_DateHourStart,
                Eve_Duration = entity.Eve_Duration,
                Eve_IdCareer = entity.Eve_IdCareer,
                Eve_IdCategory = entity.Eve_IdCategory,
                Eve_IdLocation = entity.Eve_IdLocation
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Evento", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Accion", SqlDbType.Int){Value = 4,Direction= ParameterDirection.Input},
                    new SqlParameter("@IdEvent", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input }
                }
            };
            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            List<Event> result = [];
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Evento", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Accion", SqlDbType.Int){Value = 1, Direction= ParameterDirection.Input}
                }
            };
            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(new Event
                {
                    Eve_ID = reader.GetInt32(0),
                    Eve_Description = reader["Eve_Descripcion"].ToString() ?? "",
                    Eve_DateHourStart = (DateTime)reader["Eve_FechaHoraInicio"],
                    Eve_Duration = Convert.ToInt32(reader["Eve_Duracion"]),
                    Eve_IdCareer = Convert.ToInt32(reader["Eve_Carrera"]),
                    Eve_IdCategory = Convert.ToInt32(reader["Eve_Categoria"]),
                    Eve_IdLocation = Convert.ToInt32(reader["Eve_Ubicacion"])
                });
            }
            return result;
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Evento", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Accion", SqlDbType.Int){Value = 2, Direction= ParameterDirection.Input},
                    new SqlParameter("@IdEvent", SqlDbType.Int){Value = id, Direction = ParameterDirection.Input}
                }
            };
            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                return new Event
                {
                    Eve_ID = id,
                    Eve_Description = reader["Eve_Descripcion"].ToString() ?? "",
                    Eve_DateHourStart = (DateTime)reader["Eve_FechaHoraInicio"],
                    Eve_Duration = Convert.ToInt32(reader["Eve_Duracion"]),
                    Eve_IdCareer = Convert.ToInt32(reader["Eve_Carrera"]),
                    Eve_IdCategory = Convert.ToInt32(reader["Eve_Categoria"]),
                    Eve_IdLocation = Convert.ToInt32(reader["Eve_Ubicacion"])
                };
            }
            return null;
        }

        public async Task<Event> UpdateAsync(int id, Event entity)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Evento", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters =
                {
                    new SqlParameter("@Accion", SqlDbType.Int){Value = 5, Direction = ParameterDirection.Input},
                    new SqlParameter("@IdEvent", SqlDbType.Int){Value = id, Direction = ParameterDirection.Input},
                    new SqlParameter("@DescriptionEvent", SqlDbType.VarChar){Value = entity.Eve_Description, Direction = ParameterDirection.Input},
                    new SqlParameter("@DateHourStartEvent", SqlDbType.SmallDateTime){Value = entity.Eve_DateHourStart, Direction = ParameterDirection.Input},
                    new SqlParameter("@DurationEvent", SqlDbType.Int){Value=entity.Eve_Duration, Direction = ParameterDirection.Input},
                    new SqlParameter("@LocationEvent", SqlDbType.Int){Value=entity.Eve_IdLocation, Direction = ParameterDirection.Input},
                    new SqlParameter("@CareerEvent", SqlDbType.Int){Value=entity.Eve_IdCareer, Direction = ParameterDirection.Input},
                    new SqlParameter("@CategoryEvent", SqlDbType.Int){Value=entity.Eve_IdCategory, Direction= ParameterDirection.Input}
                }
            };
            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();
            if(rowsAffected <= 0)
            {
                return null;
            }
            return new Event
            {
                Eve_ID = id,
                Eve_Description = entity.Eve_Description,
                Eve_DateHourStart = entity.Eve_DateHourStart,
                Eve_Duration = entity.Eve_Duration,
                Eve_IdCareer = entity.Eve_IdCareer,
                Eve_IdCategory = entity.Eve_IdCategory,
                Eve_IdLocation = entity.Eve_IdLocation,
            };
        }
    }
}
