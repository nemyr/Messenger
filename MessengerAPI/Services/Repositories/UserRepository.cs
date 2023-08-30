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
            //todo: check if user name exists
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

        public Task<bool> IsAccountExistsAsync(string name, string password)
        {
            return _dbContext.Accounts.AnyAsync(a => a.Name == name && a.Password == password);
        }
    }
}
