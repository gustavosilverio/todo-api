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

builder.Services.ConfigureCors();
builder.Services.AddDependencyInjection();
builder.Services.AddSwaggerGenWithAuth();
builder.Services.AddHttpContextAccessor();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ProblemExceptionHandler>();

builder.Services.AddAuthorization();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.ConfigObject.Filter = string.Empty;
        c.EnableFilter();
        c.DisplayRequestDuration();
        c.DocExpansion(DocExpansion.None);
    });
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

Log.Information("application starting...");

app.Run();