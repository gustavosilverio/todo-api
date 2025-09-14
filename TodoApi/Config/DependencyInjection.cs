using TodoApi.Repositories;
using TodoApi.Repositories.Interfaces;
using TodoApi.Services;
using TodoApi.Services.Interfaces;

namespace TodoApi.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            ConfigureServices(services);
            ConfigureRepositories(services);

            return services;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
        }

        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();
        }
    }
}