namespace TodoApi.Config
{
    internal static class CorsPolicy
    {
        public const string Default = "DefaultCorsPolicy";

        internal static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
        {
            services.AddCors(options =>
            {
                if (environment.IsProduction())
                {
                    options.AddPolicy(Default, builder =>
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
                }
                else
                {
                    options.AddPolicy(Default, builder =>
                    {
                        builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
                }
            });

            return services;
        }
    }
}
