using FogTalk.API.Configuration;
using FogTalk.Application.Configuration;
using FogTalk.Infrastructure.ServicesConfiguration;

var builder = WebApplication.CreateBuilder(args);

//loading environmental variables from .env file
DotNetEnv.Env.Load();

// Add services to the container.
builder.Services
    .AddSwaggerGen()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation()
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

#region HTTP Pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion
