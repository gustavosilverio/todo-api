using Serilog;
using Serilog.Filters;

namespace TodoApi.Config
{
    public static class SerilogConfig
    {
        public static IHostBuilder AddSerilogConfig(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Filter.ByExcluding(Matching.WithProperty<string>("RequestPath", path =>
                        path.StartsWith("/jobs") || path.StartsWith("/hangfire") || path.StartsWith("/swagger")
                    ));
            });
        }
    }
}
