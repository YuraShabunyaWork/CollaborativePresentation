using PresentationApp.Models;

namespace PresentationApp.Services.Interfases
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        Task<User> GetUserAsync(string email);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> SaveAsync();
        Task<bool> UpdateUserAsync(User user);
        Task<bool> UserExistsAsync(string email);
    }
}