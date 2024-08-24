using Dapper;
using DiarioPagosApp.Models;
using Microsoft.Data.SqlClient;

namespace DiarioPagosApp.Services
{
    public class RepositoryProduct : IRepositoryProduct
    {
        private string _connectionString;

        public RepositoryProduct(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Metodo que crea un dato en la base de datos
        /// </summary>
        /// <param name="product">Instancia del modelo</param>
        /// <returns>Id del registro</returns>
        public async Task CreateProduct(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO PRODUCTS (PRODUCT_NAME, USERS_ID) VALUES (@ProductName, @UserId);
                                                        SELECT SCOPE_IDENTITY();", product);

            product.ProductId = id;
        }

        /// <summary>
        /// Metodo que trae todos los datos de la tabla 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> ListProductsForUserId(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Product>(@"SELECT PRODUCT_NAME as ProductName, PRODUCT_ID as ProductId FROM PRODUCTS WHERE USERS_ID = @UserId;", new {UserId = userId});
        }

        /// <summary>
        /// Metodo que trae los datos de la tabla 
        /// </summary>
        /// <returns></returns>
        public async Task<Product> ListProductForId(int Id, int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Product>(@"SELECT
                                                           USERS_ID AS UserId,
                                                           PRODUCT_ID AS ProductId,
                                                           PRODUCT_NAME AS ProductName FROM PRODUCTS
                                                           WHERE PRODUCT_ID = @ProductId AND USERS_ID = @UserId;", new { ProductId = Id, UserId = userId  });
        }

        /// <summary>
        /// Metodo que valida un dato en la base de datos
        /// </summary>
        /// <param name="name">Parametro por el cual se hara la busqueda</param>
        /// <returns>Retorna 1 si existe</returns>
        public async Task<bool> IsRepeatedProduct(string name, int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            var IsRepeated = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM PRODUCTS 
                                                                              WHERE PRODUCT_NAME = @ProductName AND USERS_ID = @UserId", new { ProductName = name, UserId = userId });

            return IsRepeated == 1;
        }

        /// <summary>
        /// Meto que actualiza los datos
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task UpdateProduct(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(@"UPDATE PRODUCTS SET PRODUCT_NAME = @ProductName
                                            WHERE PRODUCT_ID = @ProductId;", product);
        }

        /// <summary>
        /// Metodo que bora un dato de la abase de datos
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteProduct(int Id, int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(@"DELETE PRODUCTS WHERE PRODUCT_ID = @ProductId AND USERS_ID = @UserId", new { ProductId = Id , UserId = userId });
        }
    }
}
