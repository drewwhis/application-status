using ApplicationStatus.Services.ApiUser;
using Microsoft.Extensions.Primitives;

namespace ApplicationStatus.API.Authentication;

public class ApiKeyEndpointFilter(IConfiguration configuration, IApiUserDataService dataService) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var success = context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var apiKey);
        if (!success || StringValues.IsNullOrEmpty(apiKey)) return TypedResults.Unauthorized();
        
        var application = await dataService.GetAuthorizedApiUser(apiKey.ToString());
        if (string.IsNullOrWhiteSpace(application)) return TypedResults.Unauthorized();
        
        return await next(context);
    }
}