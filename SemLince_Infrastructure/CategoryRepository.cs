using System.Data;
using Microsoft.Data.SqlClient;
using SemLince_Application;
using SemLince_Domain;

namespace SemLince_Infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public CategoryRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
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
                    Cat_Nombre = reader["Cat_Nombre"].ToString()
                });
            }

            return categories;
        }

        public Category GetCategoryById(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SP_SemLince", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
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
                    Cat_Nombre = reader["Cat_Nombre"].ToString()
                };
            }
            return null;
        }

        public Category CreateCategory(Category category)
        {
            using var connection = _connectionFactory.CreateConnection();
            using var command = new SqlCommand("SP_SemLince", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
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
                CommandType = System.Data.CommandType.StoredProcedure
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
                CommandType = System.Data.CommandType.StoredProcedure
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
    }
}
