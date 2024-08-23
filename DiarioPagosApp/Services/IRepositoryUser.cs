using DiarioPagosApp.Models;

namespace DiarioPagosApp.Services
{
    public interface IRepositoryUser
    {
        Task<int> CreateUser(User user);
        int GetUser();
        Task<User> SearchUserByEmail(string StandardEmail);
        Task<User> SearchUserByName(string FirstName);
    }
}
