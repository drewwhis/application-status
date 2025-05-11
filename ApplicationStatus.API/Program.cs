using ApplicationStatus.Data.Context;
using ApplicationStatus.Services.Heartbeat;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddKeyPerFile(directoryPath: "/run/secrets", optional: true);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString =
        $"server={builder.Configuration["db_host"]};"
        + $"port={builder.Configuration["db_port"]};"
        + $"uid=root;"
        + $"pwd={builder.Configuration["db_password"]};"
        + $"database={builder.Configuration["db_database"]}";
    options.UseMySQL(connectionString);
});

builder.Services.AddScoped<IHeartbeatService, HeartbeatService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Application Status")
            .WithTheme(ScalarTheme.Kepler)
            .WithLayout(ScalarLayout.Modern);
    });
}

app.UseHttpsRedirection();

app.MapGet("/heartbeat", async (IHeartbeatService service) => await service.GetAll())
    .WithName("GetHeartbeat");

app.Run();