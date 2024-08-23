using Dapper;
using DiarioPagosApp.Models;
using Microsoft.Data.SqlClient;

namespace DiarioPagosApp.Services
{
    public class RepositoryPaymentStatus : IRepositoryPaymentStatus
    {
        private readonly string _connectionString;

        public RepositoryPaymentStatus(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Metodo que traera toda la lista de los estados de pago
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PaymentStatus>> ListPaymentStatus()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<PaymentStatus>("SELECT PAYMENT_STATUS_ID AS PaymentStatusId, STATUS AS Status FROM PAYMENTS_STATUS");

        }
    }
}
