using Dapper;
using DiarioPagosApp.Models;
using Microsoft.Data.SqlClient;

namespace DiarioPagosApp.Services
{
    public class RepositorySale : IRepositorySale
    {
        private readonly string _connectionString;
        public RepositorySale(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        /// <summary>
        /// Metodo que crea un dato en la base de datos
        /// </summary>
        /// <param name="sale">Instancia del modelo</param>
        /// <returns>Id del registro</returns>
        public async Task<int> CreateSale(Sale sale)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO SALES (CUSTOMER_ID, USERS_ID, DATE_OF_SALE, PAYMENT_STATUS_ID, TOTAL_SALE) 
                                                        VALUES (@CustomerId, @UserId, @DateOfSale, @PaymentStatusId, @TotalSale);
                                                        SELECT SCOPE_IDENTITY();", sale);

            return sale.SaleId = id;
        }

        /// <summary>
        /// Metodo que trae todos los datos de la tabla 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Sale>> ListSales(int UserId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Sale>(@"SELECT 
                                                            S.SALE_ID AS SaleId, 
                                                            C.FIRST_NAME AS CUSTOMER_FIRST_NAME, 
                                                            S.DATE_OF_SALE AS DateOfSale, 
                                                            PS.STATUS AS PaymentStatusId, 
                                                            S.TOTAL_SALE AS TotalSale
                                                        FROM 
                                                            SALES S
                                                        INNER JOIN 
                                                            CUSTOMERS C ON S.CUSTOMER_ID = C.CUSTOMERS_ID
                                                        INNER JOIN 
                                                            USERS U ON S.USERS_ID = U.USERS_ID
                                                        INNER JOIN 
                                                            PAYMENTS_STATUS PS ON S.PAYMENT_STATUS_ID = PS.PAYMENT_STATUS_ID
                                                        WHERE S.USERS_ID = @UserId;", new {UserId});
        }

        /// <summary>
        /// Metodo que trae los datos de la tabla 
        /// </summary>
        /// <returns></returns>
        public async Task<Sale> ListSaleForId(int Id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Sale>(@"SELECT 
                                                                        S.SALE_ID, 
                                                                        C.FIRST_NAME AS CUSTOMER_FIRST_NAME, 
                                                                        U.FIRST_NAME AS USER_FIRST_NAME, 
                                                                        S.DATE_OF_SALE, 
                                                                        PS.STATUS, 
                                                                        S.TOTAL_SALE AS SA
                                                                    FROM 
                                                                        SALES S
                                                                    INNER JOIN 
                                                                        CUSTOMERS C ON S.CUSTOMER_ID = C.CUSTOMERS_ID
                                                                    INNER JOIN 
                                                                        USERS U ON S.USERS_ID = U.USERS_ID
                                                                    INNER JOIN 
                                                                        PAYMENTS_STATUS PS ON S.PAYMENT_STATUS_ID = PS.PAYMENT_STATUS_ID
                                                                    WHERE S.SALE_ID = @SaleId;", new { SaleId = Id });
        }

        ///// <summary>
        ///// Metodo que valida un dato en la base de datos
        ///// </summary>
        ///// <param name="name">Parametro por el cual se hara la busqueda</param>
        ///// <returns>Retorna 1 si existe</returns>
        //public async Task<bool> IsRepeatedCustomer(string email, string number)
        //{
        //    using var connection = new SqlConnection(_connectionString);
        //    var IsRepeated = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM CUSTOMERS 
        //                                                                      WHERE CUSTOMER_EMAIL = @CustomerEmail 
        //                                                                      OR PHONE_NUMBER = @PhoneNumber", new { CustomerEmail = email, PhoneNumber = number });

        //    return IsRepeated == 1;
        //}

        ///// <summary>
        ///// Meto que actualiza los datos
        ///// </summary>
        ///// <param name="customer"></param>
        ///// <returns></returns>
        //public async Task UpdateCustomer(Customer customer)
        //{
        //    using var connection = new SqlConnection(_connectionString);
        //    await connection.ExecuteAsync(@"UPDATE CUSTOMERS
        //                                        SET 
        //                                            FIRST_NAME = @FirstName,
        //                                            LAST_NAME = @LastName,
        //                                            ADDRESS = @Address,
        //                                            PHONE_NUMBER = @PhoneNumber,
        //                                            CUSTOMER_EMAIL = @CustomerEmail
        //                                        WHERE 
        //                                            CUSTOMERS_ID = @CustomerId;", customer);
        //}

        ///// <summary>
        ///// Metodo que bora un dato de la abase de datos
        ///// </summary>
        ///// <param name="Id"></param>
        ///// <returns></returns>
        //public async Task DeleteCustomer(int Id)
        //{
        //    using var connection = new SqlConnection(_connectionString);
        //    await connection.ExecuteAsync(@"DELETE CUSTOMERS WHERE CUSTOMERS_ID = @CustomerId", new { CustomerId = Id });
        //}
    }
}
