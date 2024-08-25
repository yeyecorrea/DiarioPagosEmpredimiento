using Dapper;
using DiarioPagosApp.Models;
using Microsoft.Data.SqlClient;

namespace DiarioPagosApp.Services
{
    public class RepositorySaleDetail : IRepositorySaleDetail
    {
        private readonly string _connectionString;

        public RepositorySaleDetail(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
           
        }

        /// <summary>
        /// Metodo que Crea un detalle de venta
        /// </summary>
        /// <param name="saleDetail"></param>
        /// <returns></returns>
        public async Task<int> CreateSaleDetail(SaleDetail saleDetail)
        {
            using var connection = new SqlConnection(_connectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO SALES_DETAILS (SALE_ID, PRODUCT_ID, AMOUNT, PRICE, USERS_ID) 
                                                            VALUES (@SaleId, @ProductId, @Amount, @Price, @UserId);
                                                            SELECT SCOPE_IDENTITY();", saleDetail);

            return saleDetail.SalesDetailId = id;
        }

        /// <summary>
        /// Metodo que trae todos los datos de la tabla 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SaleDetail>> ListSalesDetails(int SaleId)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<SaleDetail>(@"SELECT 
	                                                            SD.SALE_DETAIL_ID AS SalesDetailId,
	                                                            P.PRODUCT_NAME AS ProductName,
	                                                            AMOUNT AS Amount,
	                                                            PRICE AS Price
                                                            FROM 
	                                                            SALES_DETAILS SD
                                                            INNER JOIN 
	                                                            PRODUCTS P ON SD.PRODUCT_ID = P.PRODUCT_ID
                                                            WHERE SD.SALE_ID = @SaleId;", new { SaleId });
        }

    }
}
