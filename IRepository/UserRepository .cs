using CommonAPIs.Models;
using CommonAPIs.Repository;
using Microsoft.EntityFrameworkCore;

namespace CommonAPIs.ImpRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly CommomAPIsDbContext _context;
        public UserRepository(CommomAPIsDbContext context)
        {
            _context = context;
        }

        // Registration  

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Login  
        public User? GetUserByEmail(string email)
        {
            return _context.User
                .AsNoTracking()
                .FirstOrDefault(u => u.Email == email);
        }

        // Forgot Password  
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
