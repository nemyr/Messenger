using DAL.Models;
using Models.Authentification;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MessengerAPI.Services.Helpers
{
    public interface IJwtHelper
    {
        string CreateToken(Account account);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        JwtSecurityToken? ValidateToken(string token);
    }
}
