using DAL.Models;
using MessengerAPI.OptionsModels;
using MessengerAPI.Services.Helpers;
using MessengerAPI.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.Authentification;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace MessengerAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHelper jwtHelper;
        private readonly IOptions<AuthOptions> authOptions;

        public LoginController(IUserRepository userRepository, IJwtHelper jwtHelper, IOptions<AuthOptions> authOptions)
        {
            _userRepository = userRepository;
            this.jwtHelper = jwtHelper;
            this.authOptions = authOptions;
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
            var account = await _userRepository.GetAccountAsync(aData.Name, aData.Password);
            if (account is null)
                return Unauthorized();

            var encodedJwt = jwtHelper.CreateToken(account);
            var refreshToken = jwtHelper.GenerateRefreshToken();
            
            if (!await _userRepository.SetRefreshTokenAsync(account.Id, refreshToken))
                return BadRequest();

            return Ok(new AuthResult
            {
                AccessToken = encodedJwt,
                RefreshToken = refreshToken,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Refresh([NotNull]JwtApiModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var principal = jwtHelper.GetPrincipalFromExpiredToken(model.AccessToken);
            var userId = principal.FindFirstValue(authOptions.Value.ClaimId);
            if (userId == null)
                return BadRequest();

            var account = await _userRepository.GetAccountAsync(Guid.Parse(userId));
            if (account is null)
                return BadRequest();

            if(account.RefreshToken != model.RefreshToken || account.ExpireRefreshToken <= DateTime.UtcNow) 
                return BadRequest();

            var result = new JwtApiModel
            {
                AccessToken = jwtHelper.CreateToken(account),
                RefreshToken = jwtHelper.GenerateRefreshToken()
            };

            if(!await _userRepository.SetRefreshTokenAsync(account.Id, result.RefreshToken)) 
                return BadRequest();

            return Ok(result);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> LogoutAsync(Account account)
        {
            if (account is null) 
                return BadRequest();

            await _userRepository.SetRefreshTokenAsync(account.Id, string.Empty);
            return NoContent();
        }
    }
}
