using ApplicationStatus.API.Authentication;
using ApplicationStatus.API.Heartbeat;
using ApplicationStatus.Data.Context;
using ApplicationStatus.DTO.Heartbeat;
using ApplicationStatus.Services.ApiUser;
using ApplicationStatus.Services.Heartbeat;
using FluentValidation;
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

builder.Services.AddAuthorization();

// Validators
builder.Services.AddScoped<IValidator<Input>, Validator>();

// Data Services
builder.Services.AddScoped<IHeartbeatDataService, HeartbeatDataService>();
builder.Services.AddScoped<IApiUserDataService, ApiUserDataService>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    
    var service = scope.ServiceProvider.GetRequiredService<IApiUserDataService>();
    await UserSeed.Seed(service, app.Configuration);
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

app.UseAuthorization();

app.RegisterHeartbeatEndpoints();

app.Run();