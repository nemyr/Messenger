using MessengerAPI.OptionsModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MessengerAPI.Services.Extentions
{
    public static class AuthentificationServiceExtention
    {
        public static IServiceCollection AddJwtAuthentification(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var authOptions = serviceProvider.GetService<IOptions<AuthOptions>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                  options.TokenValidationParameters = new()
                  {
                      // указывает, будет ли валидироваться издатель при валидации токена
                      ValidateIssuer = true,
                      // строка, представляющая издателя
                      ValidIssuer = authOptions.Value.ISSUER,
                      // будет ли валидироваться потребитель токена
                      ValidateAudience = true,
                      // установка потребителя токена
                      ValidAudience = authOptions.Value.AUDIENCE,
                      // будет ли валидироваться время существования
                      ValidateLifetime = true,
                      // установка ключа безопасности
                      IssuerSigningKey = GetSymmetricSecurityKey(authOptions.Value.KEY),
                      // валидация ключа безопасности
                      ValidateIssuerSigningKey = true,
                  };
              });
            return services;
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key) =>
                new(Encoding.UTF8.GetBytes(key));
    }
}
