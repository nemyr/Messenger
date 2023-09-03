using DAL.Models;
using MessengerAPI.OptionsModels;
using MessengerAPI.Services.Extentions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MessengerAPI.Services.Helpers
{
    public class JwtHelper : IJwtHelper
    {
        private readonly IOptions<AuthOptions> authOptions;

        public JwtHelper(IOptions<AuthOptions> authOptions) {
            this.authOptions = authOptions;
        }

        public string CreateToken(Account account)
        {
            var claims = new List<Claim>() { new Claim(authOptions.Value.ClaimId, account.Id.ToString()) };

            var jwt = new JwtSecurityToken(
                issuer: authOptions.Value.ISSUER,
                audience: authOptions.Value.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(authOptions.Value.ExpireTime)),
                signingCredentials: new SigningCredentials(
                    AuthentificationServiceExtention.GetSymmetricSecurityKey(authOptions.Value.KEY),
                    SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public JwtSecurityToken? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthentificationServiceExtention.GetSymmetricSecurityKey(authOptions.Value.KEY),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthentificationServiceExtention.GetSymmetricSecurityKey(authOptions.Value.KEY),
                ValidateLifetime = false 
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, 
                out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
