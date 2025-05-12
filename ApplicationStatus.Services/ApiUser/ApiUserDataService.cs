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
        
        var hash = ApiKeyHasher.Hash(apiKey);
        return (await db.ApiUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Prefix == halves[0] && x.ApiKeyHash == hash))
            ?.Prefix;
    }
}