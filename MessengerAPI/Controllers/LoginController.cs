using MessengerAPI.Services.Helpers;
using MessengerAPI.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Authentification;

namespace MessengerAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHelper jwtHelper;

        public LoginController(IUserRepository userRepository, IJwtHelper jwtHelper)
        {
            _userRepository = userRepository;
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
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
