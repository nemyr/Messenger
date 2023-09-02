using DAL.Models;
using Models.Authentification;
using System.IdentityModel.Tokens.Jwt;

namespace MessengerAPI.Services.Helpers
{
    public interface IJwtHelper
    {
        string CreateToken(Account account);
        JwtSecurityToken? ValidateToken(string token);
    }
}
