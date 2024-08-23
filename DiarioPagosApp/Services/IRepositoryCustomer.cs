using DiarioPagosApp.Models;

namespace DiarioPagosApp.Services
{
    public interface IRepositoryCustomer
    {
        Task CreateCustomer(Customer customer);
        Task DeleteCustomer(int Id);
        Task<bool> IsRepeatedCustomer(string email, int userId);
        Task<Customer> ListCustomerForId(int Id, int userId);
        Task<IEnumerable<Customer>> ListCustomersForUserId(int userId);
        Task UpdateCustomer(Customer customer);
    }
}
