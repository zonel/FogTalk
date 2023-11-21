using System.Reflection;
using FogTalk.API.Configuration;
using FogTalk.Application.Abstraction;
using FogTalk.Application.Configuration;
using FogTalk.Application.Security;
using FogTalk.Infrastructure.Exceptions;
using FogTalk.Infrastructure.Persistence;
using FogTalk.Infrastructure.ServicesConfiguration;

var builder = WebApplication.CreateBuilder(args);

//loading environmental variables from .env file
DotNetEnv.Env.Load();

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

// Add services to the container.
builder.Services
    .AddSwaggerGen(s => s.IncludeXmlComments(xmlPath))
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation()
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

#region Database Seeding

// Access the database context
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var databaseSeeding = services.GetRequiredService<IDatabaseSeeder>();
        databaseSeeding.SeedData(); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
#endregion

#region HTTP Pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion
