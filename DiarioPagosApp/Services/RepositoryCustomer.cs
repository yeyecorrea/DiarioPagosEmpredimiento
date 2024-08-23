using Dapper;
using DiarioPagosApp.Models;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace DiarioPagosApp.Services
{
    public class RepositoryCustomer: IRepositoryCustomer
    {
        private readonly string _connectionString;

        public RepositoryCustomer(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        /// <summary>
        /// Metodo que crea un dato en la base de datos
        /// </summary>
        /// <param name="customer">Instancia del modelo</param>
        /// <returns>Id del registro</returns>
        public async Task CreateCustomer(Customer customer)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO CUSTOMERS (FIRST_NAME, LAST_NAME, ADDRESS, PHONE_NUMBER, CUSTOMER_EMAIL, USERS_ID) 
                                                        VALUES (@FirstName, @LastName, @Address, @PhoneNumber, @CustomerEmail, @UserId);
                                                        SELECT SCOPE_IDENTITY();", customer);

            customer.CustomerId = id;
        }

        /// <summary>
        /// Metodo que trae todos los datos de la tabla 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> ListCustomersForUserId(int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Customer>(@"SELECT 
                                                           CUSTOMERS_ID AS CustomerId, 
                                                           FIRST_NAME AS FirstName, 
                                                           LAST_NAME AS LastName, 
                                                           ADDRESS AS Address, 
                                                           PHONE_NUMBER AS PhoneNumber,
                                                           CUSTOMER_EMAIL AS CustomerEmail FROM CUSTOMERS
                                                           WHERE USERS_ID = @UserId;", new { UserId = userId });
        }

        /// <summary>
        /// Metodo que trae los datos de la tabla 
        /// </summary>
        /// <returns></returns>
        public async Task<Customer> ListCustomerForId(int CustomerId, int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Customer>(@"SELECT  
                                                           CUSTOMERS_ID AS CustomerId,
                                                           FIRST_NAME AS FirstName, 
                                                           LAST_NAME AS LastName, 
                                                           ADDRESS AS Address, 
                                                           PHONE_NUMBER AS PhoneNumber, 
                                                           CUSTOMER_EMAIL AS CustomerEmail FROM CUSTOMERS
                                                           WHERE CUSTOMERS_ID = @CustomerId AND USERS_ID = @UserId ;", new { CustomerId = CustomerId, UserId = userId});
        }

        /// <summary>
        /// Metodo que valida un dato en la base de datos
        /// </summary>
        /// <param name="name">Parametro por el cual se hara la busqueda</param>
        /// <returns>Retorna 1 si existe</returns>
        public async Task<bool> IsRepeatedCustomer(string email, int userId)
        {
            using var connection = new SqlConnection(_connectionString);
            var IsRepeated = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM CUSTOMERS 
                                                                              WHERE CUSTOMER_EMAIL = @CustomerEmail 
                                                                              AND USERS_ID = @UserId", new { CustomerEmail = email, UserId = userId });

            return IsRepeated == 1;
        }

        /// <summary>
        /// Meto que actualiza los datos
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task UpdateCustomer(Customer customer)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(@"UPDATE CUSTOMERS
                                                SET 
                                                    FIRST_NAME = @FirstName,
                                                    LAST_NAME = @LastName,
                                                    ADDRESS = @Address,
                                                    PHONE_NUMBER = @PhoneNumber,
                                                    CUSTOMER_EMAIL = @CustomerEmail
                                                WHERE 
                                                    CUSTOMERS_ID = @CustomerId;", customer);
        }

        /// <summary>
        /// Metodo que bora un dato de la abase de datos
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteCustomer(int Id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(@"DELETE CUSTOMERS WHERE CUSTOMERS_ID = @CustomerId", new {CustomerId = Id});
        }
    }
}
