using DiarioPagosApp.Models;

namespace DiarioPagosApp.Services
{
    public interface IRepositoryProduct
    {
        Task CreateProduct(Product product);
        Task DeleteProduct(int Id);
        Task<bool> IsRepeatedProduct(string name);
        Task<Product> ListProductForId(int Id);
        Task<IEnumerable<Product>> ListProducts();
        Task UpdateProduct(Product product);
    }
}
