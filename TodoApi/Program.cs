using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using TodoApi.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);

    //c.MapType<ProblemDetails>(() => new() { Type = "object" });
});

builder.Services.ConfigureCors();
builder.Services.AddDependencyInjection();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ProblemExceptionHandler>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .CreateLogger();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();