using System.Reflection;
using PokemonTryASP.Interface;
using PokemonTryASP.Repository;
using DbUp;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

using var scoped = app.Services.CreateScope();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Migrate(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void Migrate(IHost host)
{
    using var scope = host.Services.CreateScope();

    var services = scope.ServiceProvider;
    var configuration = services.GetRequiredService<IConfiguration>();
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    logger.LogInformation("Migrating...");

    string connection = configuration.GetConnectionString("DefaultConnection")!;

    EnsureDatabase.For.PostgresqlDatabase(connection);

    var upgrader = DeployChanges.To
        .PostgresqlDatabase(connection)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToConsole()
        .Build();

    var result = upgrader.PerformUpgrade();

    if (!result.Successful)
    {
        logger.LogError(result.Error, "An error");
        return;
    }
    
    logger.LogInformation("Migration complete");
}