using DAL.Models;

namespace MessengerAPI.Services.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsAccountExistsAsync(string name, string password);
        Task<bool> CreateAccountAsync(string name, string password);
        Task<Account?> GetAccountAsync(Guid id);
        Task<Account?> GetAccountAsync(string name, string password);
        Task<bool> SetRefreshTokenAsync(Guid userId, string token);
        Task<User?> GetUserByAccountIdAsync(Guid accountId);
    }
}
