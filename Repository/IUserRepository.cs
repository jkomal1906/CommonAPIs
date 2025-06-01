using CommonAPIs.Models;

namespace CommonAPIs.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);

        Task<User> AddUserAsync(User user);

        User GetUserByEmail(string email);

        Task<User> GetUserByEmailAsync(string email);

    }
}
