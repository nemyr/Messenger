using MessengerAPI.OptionsModels;
using MessengerAPI.Services.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MessengerAPI.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserDataMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserRepository userRepository;
        private readonly IOptions<AuthOptions> options;

        public UserDataMiddleware(RequestDelegate next, IUserRepository userRepository, IOptions<AuthOptions> options)
        {
            _next = next;
            this.userRepository = userRepository;
            this.options = options;
        }

        public Task Invoke(HttpContext httpContext)
        {

            if (!httpContext.User.Identity?.IsAuthenticated ?? false)
                return _next(httpContext);

            var uId = httpContext.User.FindFirstValue(options.Value.ClaimId);
            if (uId == null)
                return _next(httpContext);
            var userGuid = Guid.Parse(uId);
            
            var accountTask = userRepository.GetAccountAsync(userGuid);
            var userTask = userRepository.GetUserByAccountIdAsync(userGuid);
            Task.WaitAll(accountTask, userTask);
            httpContext.Items["Account"] = accountTask.Result;
            httpContext.Items["User"] = userTask.Result;

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class UserDataMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserDataMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserDataMiddleware>();
        }
    }
}
