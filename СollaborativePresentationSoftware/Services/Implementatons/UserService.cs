using Microsoft.EntityFrameworkCore;
using PresentationApp.Data;
using PresentationApp.Models;
using PresentationApp.Services.Interfases;

namespace PresentationApp.Services.Implementatons
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<bool> CreateUserAsync(User user)
        {
            await _context.AddAsync(user);
            return await SaveAsync();
        }
        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Remove(user);
            return await SaveAsync();
        }
        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Update(user);
            return await SaveAsync();
        }
    }
}
