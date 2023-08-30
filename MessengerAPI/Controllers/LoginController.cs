using MessengerAPI.OptionsModels;
using MessengerAPI.Services.Extentions;
using MessengerAPI.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Authentification;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MessengerAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<AuthOptions> _authOptions;

        public LoginController(IUserRepository userRepository, IOptions<AuthOptions> authOptions)
        {
            _userRepository = userRepository;
            _authOptions = authOptions;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AuthData authData)
        {
            try
            {
                if (await _userRepository.CreateAccountAsync(authData.Name, authData.Password))
                    return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthData aData)
        {
            if (!await _userRepository.IsAccountExistsAsync(aData.Name, aData.Password))
                return Unauthorized();

            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, aData.Name) };

            var jwt = new JwtSecurityToken(
            issuer: _authOptions.Value.ISSUER,
            audience: _authOptions.Value.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(
                AuthentificationServiceExtention.GetSymmetricSecurityKey(_authOptions.Value.KEY),
                SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new AuthResult { AccessToken = encodedJwt, UserName = aData.Name });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
