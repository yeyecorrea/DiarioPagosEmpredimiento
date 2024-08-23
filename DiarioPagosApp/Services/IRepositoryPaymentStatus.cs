using DiarioPagosApp.Models;

namespace DiarioPagosApp.Services
{
    public interface IRepositoryPaymentStatus
    {
        Task<IEnumerable<PaymentStatus>> ListPaymentStatus();
    }
}
