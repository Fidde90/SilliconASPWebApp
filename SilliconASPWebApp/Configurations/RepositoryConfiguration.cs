using Infrastructure.Repositories;
using Infrastructure.Services;

namespace SilliconASPWebApp.Configurations
{
    public static class RepositoryConfiguration
    {
        public static void RegisterRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<UserRepository>();
            services.AddScoped<AddressRepository>();
        }
    }
}
