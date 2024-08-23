using DiarioPagosApp.Models;

namespace DiarioPagosApp.Services
{
    public interface IRepositorySale
    {
        Task<int> CreateSale(Sale sale);
        Task<Sale> ListSaleForId(int Id);
        Task<IEnumerable<Sale>> ListSales(int UserId);
    }
}
