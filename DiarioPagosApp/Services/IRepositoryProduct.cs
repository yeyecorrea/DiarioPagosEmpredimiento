using DiarioPagosApp.Models;

namespace DiarioPagosApp.Services
{
    public interface IRepositoryProduct
    {
        Task CreateProduct(Product product);
        Task DeleteProduct(int Id, int userId);
        Task<bool> IsRepeatedProduct(string name, int userId);
        Task<Product> ListProductForId(int Id, int userId);
        Task<IEnumerable<Product>> ListProductsForUserId(int userId);
        Task UpdateProduct(Product product);
    }
}
