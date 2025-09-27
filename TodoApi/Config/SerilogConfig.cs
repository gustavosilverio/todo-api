using Serilog;

namespace TodoApi.Config
{
    public static class SerilogConfig
    {
        public static IHostBuilder AddSerilogConfig(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseSerilog((context, services, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}
