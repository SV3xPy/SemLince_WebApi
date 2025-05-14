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
    public class PersonaRepository : IPersonaRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public PersonaRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Persona> AddAsync(Persona entity)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Persona", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters = {
                    new SqlParameter("@Accion", SqlDbType.Int){Value = 3, Direction = ParameterDirection.Input},
                    new SqlParameter("@Name_Persona" , SqlDbType.VarChar) {Value = entity.Per_Name, Direction = ParameterDirection.Input},
                    new SqlParameter("@PaternalS_Persona", SqlDbType.VarChar) {Value = entity.Per_PaternalSurname, Direction = ParameterDirection.Input} ,
                    new SqlParameter("@MaternalS_Persona", SqlDbType.VarChar) {Value = entity.Per_MaternalSurname, Direction = ParameterDirection.Input} ,
                    new SqlParameter("@Ncontrol_Persona", SqlDbType.Int) {Value = entity.Per_NControl, Direction = ParameterDirection.Input} ,
                    new SqlParameter("@Email_Persona" , SqlDbType.NVarChar) {Value = entity.Per_Email, Direction = ParameterDirection.Input} ,
                    new SqlParameter("@Password_Persona", SqlDbType.NVarChar){Value = entity.Per_Password, Direction = ParameterDirection.Input} ,
                    new SqlParameter("@Semester_Persona", SqlDbType.TinyInt){Value = entity.Per_Semester, Direction = ParameterDirection.Input} ,
                    new SqlParameter("@Carrer_Persona", SqlDbType.Int){Value = entity.Per_Carrer, Direction = ParameterDirection.Input}
                }

            };
            await connection.OpenAsync();

            int newId = Convert.ToInt32(await command.ExecuteScalarAsync());
            if (newId == 0)
            {
                throw new Exception("Error al insertar.");
            }
            return new Persona
            {
                Per_ID = newId,
                Per_Name = entity.Per_Name,
                Per_PaternalSurname = entity.Per_PaternalSurname,
                Per_MaternalSurname = entity.Per_MaternalSurname,
                Per_Email = entity.Per_Email,
                Per_Password = "",
                Per_Carrer = entity.Per_Carrer,
                Per_NControl = entity.Per_NControl,
                Per_Semester = entity.Per_Semester,
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Persona", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters = {
                    new SqlParameter("@Accion", SqlDbType.Int){Value = 4, Direction = ParameterDirection.Input},
                    new SqlParameter("@Id_Persona", SqlDbType.Int){Value = id, Direction = ParameterDirection.Input}
                }
            };
            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Persona>> GetAllAsync()
        {
            List<Persona> result = [];
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Persona", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters = {
                    new SqlParameter("@Accion", SqlDbType.Int){Value = 1, Direction = ParameterDirection.Input},
                }
            };
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(new Persona
                {
                    Per_ID = reader.GetInt32(0),
                    Per_Name = reader["Per_Nombre"].ToString() ?? "",
                    Per_PaternalSurname = reader["Per_ApellidoPaterno"].ToString() ?? "",
                    Per_MaternalSurname = reader["Per_ApellidoMaterno"].ToString() ?? "",
                    Per_Email = reader["Per_Correo"].ToString() ?? "",
                    Per_Password = "",
                    Per_Carrer = Convert.ToInt32(reader["Per_Carrera"]),
                    Per_NControl = Convert.ToInt32(reader["Per_NumeroControl"]),
                    Per_Semester = Convert.ToInt32(reader["Per_Semestre"])
                });
            }
            return result;

        }

        public async Task<Persona> GetByIdAsync(int id)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Persona", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters = {
                    new SqlParameter("@Accion", SqlDbType.Int){Value = 2, Direction = ParameterDirection.Input},
                    new SqlParameter("@Id_Persona", SqlDbType.Int){Value = id, Direction = ParameterDirection.Input}
                }
            };
            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                return new Persona
                {
                    Per_ID = reader.GetInt32(0),
                    Per_Name = reader["Per_Nombre"].ToString() ?? "",
                    Per_PaternalSurname = reader["Per_ApellidoPaterno"].ToString() ?? "",
                    Per_MaternalSurname = reader["Per_ApellidoMaterno"].ToString() ?? "",
                    Per_Email = reader["Per_Correo"].ToString() ?? "",
                    Per_Password = "",
                    Per_Carrer = Convert.ToInt32(reader["Per_Carrera"]),
                    Per_NControl = Convert.ToInt32(reader["Per_NumeroControl"]),
                    Per_Semester = Convert.ToInt32(reader["Per_Semestre"])
                };
            }
            return null;
        }

        public Task<Persona> LoginAsync(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public async Task<Persona> UpdateAsync(int id, Persona entity)
        {
            using SqlConnection connection = _connectionFactory.CreateSqlServerConnection();
            using SqlCommand command = new("SP_SemLince_Persona", connection)
            {
                CommandType = CommandType.StoredProcedure,
                Parameters = {
                    new SqlParameter("@Accion", SqlDbType.Int){Value = 5, Direction = ParameterDirection.Input},
                    new SqlParameter("@Id_Persona", SqlDbType.Int){Value = id, Direction = ParameterDirection.Input},
                    new SqlParameter("@Name_Persona" , SqlDbType.VarChar) {Value = entity.Per_Name, Direction = ParameterDirection.Input},
                    new SqlParameter("@PaternalS_Persona", SqlDbType.VarChar) {Value = entity.Per_PaternalSurname, Direction = ParameterDirection.Input} ,
                    new SqlParameter("@MaternalS_Persona", SqlDbType.VarChar) {Value = entity.Per_MaternalSurname, Direction = ParameterDirection.Input} ,
                    new SqlParameter("@Ncontrol_Persona", SqlDbType.Int) {Value = entity.Per_NControl, Direction = ParameterDirection.Input} ,
                    new SqlParameter("@Email_Persona" , SqlDbType.NVarChar) {Value = entity.Per_Email, Direction = ParameterDirection.Input} ,
                    new SqlParameter("@Password_Persona", SqlDbType.NVarChar){Value = entity.Per_Password, Direction = ParameterDirection.Input} ,
                    new SqlParameter("@Semester_Persona", SqlDbType.TinyInt){Value = entity.Per_Semester, Direction = ParameterDirection.Input} ,
                    new SqlParameter("@Carrer_Persona", SqlDbType.Int){Value = entity.Per_Carrer, Direction = ParameterDirection.Input}
                }
            };
            await connection.OpenAsync();

            int rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected <= 0)
            {
                return null;
            }
            return new Persona
            {
                Per_ID = id,
                Per_Name = entity.Per_Name,
                Per_PaternalSurname = entity.Per_PaternalSurname,
                Per_MaternalSurname = entity.Per_MaternalSurname,
                Per_Email = entity.Per_Email,
                Per_Password = "",
                Per_Carrer = entity.Per_Carrer,
                Per_NControl = entity.Per_NControl,
                Per_Semester = entity.Per_Semester,
            };
        }
    }
}
