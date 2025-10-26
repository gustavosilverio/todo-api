using Microsoft.IdentityModel.JsonWebTokens;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IdentityModel.Tokens.Jwt;
using TodoApi.Config;

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Host.AddSerilogConfig();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureCors(builder.Configuration);
builder.Services.AddDependencyInjection();
builder.Services.AddSwaggerGenWithAuth();
builder.Services.AddHttpContextAccessor();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ProblemExceptionHandler>();
builder.Services.AddHangfireServices(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);

var app = builder.Build();

Log.Information("Starting application...");

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.ScheduleAllJobs();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseCors(CorsPolicy.Development);

    app.AddHangfireDashboard();

    app.UseSwaggerUI(o =>
    {
        o.DocumentTitle = "TodoAPI";
        o.ConfigObject.Filter = string.Empty;
        o.EnableFilter();
        o.DisplayRequestDuration();
        o.DocExpansion(DocExpansion.None);
    });
}
else
{
    app.UseCors(CorsPolicy.Production);

    app.UseSwaggerUI(o =>
    {
        o.DocumentTitle = "TodoAPI";
        o.ConfigObject.Filter = string.Empty;
        o.EnableFilter();
        o.DocExpansion(DocExpansion.None);
        o.SupportedSubmitMethods();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();