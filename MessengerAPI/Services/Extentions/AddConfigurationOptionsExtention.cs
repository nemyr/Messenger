using MessengerAPI.OptionsModels;

namespace MessengerAPI.Services.Extentions
{
    public static class AddConfigurationOptionsExtention
    {
        public static IServiceCollection AddConfigurationOptions(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.Configure<AuthOptions>(builder.Configuration.GetSection("AuthOptions"));

            return services;
        }
    }
}
