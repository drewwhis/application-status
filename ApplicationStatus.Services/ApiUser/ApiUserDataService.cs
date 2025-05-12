using ApplicationStatus.Data.Context;
using ApplicationStatus.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationStatus.Services.ApiUser;

public class ApiUserDataService(AppDbContext db) : IApiUserDataService
{
    public async Task<string?> GetAuthorizedApiUser(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey)) return null;
        if (!apiKey.Contains('.')) return null;
        
        var halves = apiKey.Split('.');
        if (halves.Length != 2) return null;
        if (halves[0].Length != 7) return null;
        
        var hash = ApiKeyHasher.Hash(apiKey);
        return (await db.ApiUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Prefix == halves[0] && x.ApiKeyHash == hash))
            ?.Prefix;
    }

    public async Task<Models.ApiUser?> CreateUser(string applicationName, string apiKey, string prefix)
    {
        var apiUser = new Models.ApiUser
        {
            Id = Guid.CreateVersion7(),
            ApplicationName = applicationName,
            Prefix = prefix,
            ApiKeyHash = ApiKeyHasher.Hash(apiKey)
        };

        try
        {
            await db.ApiUsers.AddAsync(apiUser);
            await db.SaveChangesAsync();
            await db.ApiUsers.Entry(apiUser).ReloadAsync();
        }
        catch
        {
            return null;
        }
        
        return apiUser;
    }
}