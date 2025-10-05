using Microsoft.AspNetCore.Identity;
using System.Reflection;
using TodoApi.Data;
using TodoApi.Model;
using TodoApi.Models;
using TodoApi.Service;
using TodoApi.Util;

namespace TodoApi.Config
{
    internal static class DependencyInjection
    {
        internal static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            ConfigureServices(services);
            ConfigureRepositories(services);
            ConfigureUtils(services);
            ConfigureExternalDependencies(services);

            return services;
        }

        internal static void ConfigureServices(IServiceCollection services)
        {
            // Get all interfaces in the assembly of the specified class
            var interfaces = Assembly.GetAssembly(typeof(TodoService))!.GetTypes()
                .Where(r => r.IsInterface)
                .ToList();

            // For each interface, find its implementation and register it in the DI container
            // In the filter, we exclude interfaces and abstract classes
            foreach (Type interfaceType in interfaces)
            {
                var implementation = Assembly.GetAssembly(typeof(TodoService))!.GetTypes()
                    .Where(r => interfaceType.IsAssignableFrom(r) && !r.IsInterface && !r.IsAbstract)
                    .ToList()
                    .FirstOrDefault();

                if (implementation is null)
                    throw new ArgumentException(RetrieveMessageErrorNotImplemented(interfaceType.Name, Assembly.GetAssembly(typeof(TodoService))!.FullName!.Split(',')[0]), nameof(implementation));
            
                services.AddTransient(interfaceType, implementation);
            }
        }

        internal static void ConfigureRepositories(IServiceCollection services)
        {
            // Get all interfaces in the assembly of the specified class
            var interfaces = Assembly.GetAssembly(typeof(BaseRepository))!.GetTypes()
                .Where(r => r.IsInterface)
                .ToList();

            // For each interface, find its implementation and register it in the DI container
            // In the filter, we exclude interfaces and abstract classes
            foreach (Type interfaceType in interfaces)
            {
                var implementation = Assembly.GetAssembly(typeof(BaseRepository))!.GetTypes()
                    .Where(r => interfaceType.IsAssignableFrom(r) && !r.IsInterface && !r.IsAbstract)
                    .ToList()
                    .FirstOrDefault();

                if (implementation is null)
                    throw new ArgumentException(RetrieveMessageErrorNotImplemented(interfaceType.Name, Assembly.GetAssembly(typeof(BaseRepository))!.FullName!.Split(',')[0]), nameof(implementation));

                services.AddTransient(interfaceType, implementation);
            }
        }

        internal static void ConfigureUtils(IServiceCollection services)
        {
            // Get all interfaces in the assembly of the specified class
            var interfaces = Assembly.GetAssembly(typeof(PasswordHash))!.GetTypes()
                .Where(r => r.IsInterface)
                .ToList();

            // For each interface, find its implementation and register it in the DI container
            // In the filter, we exclude interfaces and abstract classes
            foreach (Type interfaceType in interfaces)
            {
                var implementation = Assembly.GetAssembly(typeof(PasswordHash))!.GetTypes()
                    .Where(r => interfaceType.IsAssignableFrom(r) && !r.IsInterface && !r.IsAbstract)
                    .ToList()
                    .FirstOrDefault();

                if (implementation is null)
                    throw new ArgumentException(RetrieveMessageErrorNotImplemented(interfaceType.Name, Assembly.GetAssembly(typeof(PasswordHash))!.FullName!.Split(',')[0]), nameof(implementation));

                services.AddTransient(interfaceType, implementation);
            }
        }

        internal static void ConfigureExternalDependencies(IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<UserClaimsInfo>();
        }

        /// <summary>
        /// Default invalid DI error message
        /// </summary>
        /// <param name="interfaceName">Name of the current inferface</param>
        /// <param name="layer">Project layer</param>
        /// <returns></returns>
        private static string RetrieveMessageErrorNotImplemented(string interfaceName, string layer)
        {
            return $"Interface '{interfaceName}' don't have an implementation. Please create an implementation! Project: '{layer}'";
        }
    }
}