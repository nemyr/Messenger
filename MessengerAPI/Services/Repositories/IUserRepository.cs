namespace MessengerAPI.Services.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsAccountExistsAsync(string name, string password);
        Task<bool> CreateAccountAsync(string name, string password);
    }
}
