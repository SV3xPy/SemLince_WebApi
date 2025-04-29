using System.Data;
using Microsoft.Data.SqlClient;
using SemLince_Application.IRepositories;
using SemLince_Domain.Entities;

namespace SemLince_Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public CategoryRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        /*
         * IMPLEMENTACION ANTERIOR NO ASINCRONA
        public List<Category> GetAllCategories()
        {
            List<Category> categories = [];
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SP_SemLince", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 1, Direction = ParameterDirection.Input });

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Cat_ID = Convert.ToInt32(reader["Cat_ID"]),
                    Cat_Nombre = reader["Cat_Nombre"].ToString() ?? ""
                });
            }

            return categories;
        }

        public Category GetCategoryById(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SP_SemLince", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 2, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@IdCategory", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input });

            connection.Open();

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Category
                {
                    Cat_ID = Convert.ToInt32(reader["Cat_ID"]),
                    Cat_Nombre = reader["Cat_Nombre"].ToString() ?? ""
                };
            }
            return null;
        }

        public Category CreateCategory(Category category)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SP_SemLince", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 3, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@NameCategory", SqlDbType.NVarChar) { Value = category.Cat_Nombre, Direction = ParameterDirection.Input });

            connection.Open();

            var newId = Convert.ToInt32(command.ExecuteScalar()); // Obtener el ID generado

            if (newId == 0)
            {
                throw new Exception("Error al insertar la categoría. No se generó un ID.");
            }

            return new Category
            {
                Cat_ID = newId,
                Cat_Nombre = category.Cat_Nombre
            };
        }

        public bool DeleteCategory(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SP_SemLince", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 4, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@IdCategory", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input });

            connection.Open();
            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public Category UpdateCategory(int id, Category category)
        {
            if (GetCategoryById(id) == null) {
                return null;
            }
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SP_SemLince", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 5, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@IdCategory", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@NameCategory", SqlDbType.NVarChar) { Value = category.Cat_Nombre, Direction = ParameterDirection.Input });

            connection.Open();
            var rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected <=0)
            {
                return null;
            }
            return new Category
            {
                Cat_ID = id,
                Cat_Nombre = category.Cat_Nombre
            };
        }

        */

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            List<Category> categories = [];
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SP_SemLince", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 1, Direction = ParameterDirection.Input });
            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Cat_ID = Convert.ToInt32(reader["Cat_ID"]),
                    Cat_Nombre = reader["Cat_Nombre"].ToString() ?? ""
                });
            }

            return categories;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SP_SemLince", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 2, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@IdCategory", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input });
            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            if (reader.Read())
            {
                return new Category
                {
                    Cat_ID = Convert.ToInt32(reader["Cat_ID"]),
                    Cat_Nombre = reader["Cat_Nombre"].ToString() ?? ""
                };
            }
            return null;
        }

        public async Task<Category> AddAsync(Category entity)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SP_SemLince", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 3, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@NameCategory", SqlDbType.NVarChar) { Value = entity.Cat_Nombre, Direction = ParameterDirection.Input });
            await connection.OpenAsync();

            var newId = Convert.ToInt32(await command.ExecuteScalarAsync()); // Obtener el ID generado

            if (newId == 0)
            {
                throw new Exception("Error al insertar la categoría. No se generó un ID.");
            }

            return new Category
            {
                Cat_ID = newId,
                Cat_Nombre = entity.Cat_Nombre
            };
        }

        public async Task<Category> UpdateAsync(int id, Category entity)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SP_SemLince", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 5, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@IdCategory", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@NameCategory", SqlDbType.NVarChar) { Value = entity.Cat_Nombre, Direction = ParameterDirection.Input });
            await connection.OpenAsync();

            var rowsAffected = await command.ExecuteNonQueryAsync();
            if (rowsAffected <= 0)
            {
                return null;
            }
            return new Category
            {
                Cat_ID = id,
                Cat_Nombre = entity.Cat_Nombre
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SP_SemLince", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@Accion", SqlDbType.Int) { Value = 4, Direction = ParameterDirection.Input });
            command.Parameters.Add(new SqlParameter("@IdCategory", SqlDbType.Int) { Value = id, Direction = ParameterDirection.Input });
            await connection.OpenAsync();

            return await command.ExecuteNonQueryAsync() > 0;
        }
    }
}
