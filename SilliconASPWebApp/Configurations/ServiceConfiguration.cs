using Infrastructure.Services;

namespace SilliconASPWebApp.Configurations
{
    public static class ServiceConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddScoped<UserService>();
            services.AddScoped<AuthService>();
            services.AddScoped<AccountService>();
            services.AddScoped<AddressService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<CourseService>();

        }
    }
}
