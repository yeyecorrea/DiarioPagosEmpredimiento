using DiarioPagosApp.Models;

namespace DiarioPagosApp.Services
{
    public interface IRepositorySaleDetail
    {
        Task<int> CreateSaleDetail(SaleDetail saleDetail);
        Task<IEnumerable<SaleDetail>> ListSalesDetails(int SaleId);
    }
}
