using ApplicationStatus.DTO.Heartbeat;
using ApplicationStatus.Services.Heartbeat;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ApplicationStatus.API.Heartbeat;

public static class Endpoints
{
    public static void RegisterHeartbeatEndpoints(this WebApplication app)
    {
        var items = app
            .MapGroup("/heartbeats")
            .WithTags("Heartbeats");
        
        items.MapGet("/", GetAll);
        items.MapGet("/{applicationName}", Get);
        items.MapPost("/", Create);
        items.MapPatch("/", Update);
    }

    private static async Task<Results<Ok<Output>, NotFound, ValidationProblem>> Update(IHeartbeatDataService dataService,
        IValidator<Input> validator, Input heartbeat)
    {
        var validationResult = await validator.ValidateAsync(heartbeat);
        if (!validationResult.IsValid) 
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }
        
        var model = await dataService.Get(heartbeat.ApplicationName);
        if (model is null) return TypedResults.NotFound();
        
        var updatedModel = model.UpdateModel(heartbeat);
        return TypedResults.Ok((await dataService.Update(updatedModel)).ToOutput());
    }

    private static async Task<Results<Ok<Output>, NotFound>> Get(IHeartbeatDataService dataService,
        string applicationName)
    {
        var result = await dataService.Get(applicationName);
        return result is null ? TypedResults.NotFound() : TypedResults.Ok(result.ToOutput());
    }

    private static async Task<Results<Ok<IEnumerable<Output>>, NotFound>> GetAll(IHeartbeatDataService dataService)
    {
        var result = await dataService.GetAll();
        return TypedResults.Ok(result.Select(h => h.ToOutput()));
    }

    private static async Task<Results<Ok<Output>, ValidationProblem, BadRequest<string>>> Create(IValidator<Input> validator, IHeartbeatDataService dataService, Input heartbeat)
    {
        var validationResult = await validator.ValidateAsync(heartbeat);
        if (!validationResult.IsValid) 
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }
        
        var exists = await dataService.Get(heartbeat.ApplicationName) is not null;
        if (exists) return TypedResults.BadRequest("Duplicate entry with same name exists");
            
        var model = heartbeat.ToModel();
        var result = await dataService.Create(model);
        return TypedResults.Ok(result.ToOutput());
    }
}