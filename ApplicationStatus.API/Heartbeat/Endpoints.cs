using ApplicationStatus.DTO.Heartbeat;
using ApplicationStatus.Services.Heartbeat;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ApplicationStatus.API.Heartbeat;

public static class HeartbeatEndpoints
{
    public static void RegisterHeartbeatEndpoints(this WebApplication app)
    {
        var items = app
            .MapGroup("/heartbeats")
            .WithTags("Heartbeats");
        
        items.MapGet("/", GetAll);
        items.MapPost("/", Create);
    }

    private static async Task<Results<Ok<IEnumerable<Output>>, NotFound>> GetAll(IHeartbeatDataService dataService)
    {
        var result = await dataService.GetAll();
        return TypedResults.Ok(result);
    }

    private static async Task<Results<Ok<Output>, ValidationProblem>> Create(IValidator<Input> validator, IHeartbeatDataService dataService, Input heartbeat)
    {
        var validationResult = await validator.ValidateAsync(heartbeat);
        if (!validationResult.IsValid) 
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }

        return TypedResults.Ok(await dataService.Create(heartbeat));
    }
}