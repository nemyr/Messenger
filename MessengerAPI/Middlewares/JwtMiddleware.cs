using MessengerAPI.OptionsModels;
using MessengerAPI.Services.Helpers;
using MessengerAPI.Services.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MessengerAPI.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtHelper jwtHelper;
        private readonly IUserRepository userRepository;
        private readonly IOptions<AuthOptions> options;

        public JwtMiddleware(RequestDelegate next, IJwtHelper jwtHelper, IUserRepository userRepository, IOptions<AuthOptions> options)
        {
            _next = next;
            this.jwtHelper = jwtHelper;
            this.userRepository = userRepository;
            this.options = options;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token is not null)
            {
                try
                {
                    var securityToken = jwtHelper.ValidateToken(token);
                    if (securityToken is not null)
                    {
                        var userId = securityToken.Claims.First(x => x.Type == options.Value.ClaimId).Value;
                        var userTask = userRepository.GetAccountAsync(Guid.Parse(userId));
                        userTask.Wait();
                        httpContext.Items["User"] = userTask.Result;
                    }
                } catch (SecurityTokenException)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized; 
                    return Task.CompletedTask;
                }
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
