namespace TodoApi.Config
{
    internal static class CorsPolicy
    {
        public const string Development = "Development";
        public const string Production = "Production";

        internal static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Development, builder =>
                {
                    builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });

                options.AddPolicy(Production, builder =>
                {
                    var frontendUrl = config["FrontendUrl"];

                    if (string.IsNullOrEmpty(frontendUrl))
                    {
                        throw new InvalidOperationException("FrontendUrl is not configured in appsettings.json for Production environment.");
                    }

                    builder.WithOrigins(frontendUrl)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
