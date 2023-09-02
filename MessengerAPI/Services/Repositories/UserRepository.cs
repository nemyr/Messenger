using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MessengerAPI.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(IDbContextFactory<ApplicationContext> dbContextFactory)
        {
            _dbContext = dbContextFactory.CreateDbContext();
        }

        public async Task<bool> CreateAccountAsync(string name, string password)
        {
            if (await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Name == name) != null)
                return false;

            Account account = new ()
            {
                Name = name,
                Password = password,
                RegisteredDate = DateTime.UtcNow,
            };

            User user = new ()
            {
                Name = name,
            };

            account.User = user;

            await _dbContext.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<Account?> GetAccountAsync(Guid id) => 
            _dbContext.Accounts.FirstOrDefaultAsync(u => u.Id == id);

        public Task<Account?> GetAccountAsync(string name, string password) => 
            _dbContext.Accounts.FirstOrDefaultAsync(a => a.Name == name && a.Password == password);

        public Task<bool> IsAccountExistsAsync(string name, string password) => 
            _dbContext.Accounts.AnyAsync(a => a.Name == name && a.Password == password);
    }
}
