using DAL.Models;
using MessengerAPI.OptionsModels;
using MessengerAPI.Services.Extentions;
using MessengerAPI.Services.Helpers;
using MessengerAPI.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IJwtHelper jwtHelper;

        public LoginController(IUserRepository userRepository, IOptions<AuthOptions> authOptions, IJwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _authOptions = authOptions;
            this.jwtHelper = jwtHelper;
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
            
            return Ok(new AuthResult { AccessToken = encodedJwt, UserName = aData.Name });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            Account user = (Account)HttpContext.Items["User"];
            return Ok();
        }
    }
}
