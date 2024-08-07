using DiarioPagosApp.Models;

namespace DiarioPagosApp.Services
{
    public interface IRepositoryCustomer
    {
        Task CreateCustomer(Customer customer);
        Task DeleteCustomer(int Id);
        Task<bool> IsRepeatedCustomer(string email, string number);
        Task<Customer> ListCustomerForId(int Id);
        Task<IEnumerable<Customer>> ListCustomers();
        Task UpdateCustomer(Customer customer);
    }
}
