using Hangfire;
using Serilog;
using System.Reflection;
using TodoApi.Jobs;
using TodoApi.Jobs.Attributes;
using TodoApi.Jobs.Interfaces;

namespace TodoApi.Config
{
    public static class Hangfire
    {
        public static void AddHangfireServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddHangfire(globalConfig =>
            {
                globalConfig
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(config.GetConnectionString("dbTodo"));
            })
            .AddHangfireServer();
        }

        public static void AddHangfireDashboard(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                DashboardTitle = "TodoAPI - Jobs",
            });
        }

        public static void ScheduleAllJobs(this IApplicationBuilder app)
        {
            var assembly = typeof(CleanTodos).Assembly;

            var jobs = assembly
                .GetTypes()
                .Where(t => typeof(IJob).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(j => new { Type = j, Attribute = j.GetCustomAttribute<Job>() });

            if (!jobs.Any())
            {
                Log.Information("No jobs configured");
                return;
            }

            using var scope = app.ApplicationServices.CreateScope();
            var provider = scope.ServiceProvider;

            var jobManager = provider.GetRequiredService<IRecurringJobManager>();

            foreach (var job in jobs)
            {
                if (job.Attribute is null)
                {
                    Log.Error("Job {0} don't have Job attribute.", job.Type.Name);
                    throw new Exception();
                }

                jobManager.AddOrUpdate(
                    job.Attribute?.JobId ?? job.Type.Name!,
                    () => ((IJob)provider.GetRequiredService(job.Type)).Execute(),
                    job.Attribute?.Cron
                );
            }
        }
    }
}
