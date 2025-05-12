using ApplicationStatus.Services.ApiUser;

namespace ApplicationStatus.API.Authentication;

public static class UserSeed
{
    public static async Task Seed(IApiUserDataService dataService, IConfiguration configuration)
    {
        var userString = configuration["users"];
        if (string.IsNullOrWhiteSpace(userString)) return;
        
        var userList = userString
            .Split(Environment.NewLine)
            .Select(s =>
            {
                var parts = s.Trim().Split(" ").Select(ss => ss.Trim()).ToArray();
                if (parts.Length != 2) throw new ArgumentException();
                return (User: parts[0], Key: parts[1]);
            })
            .ToList();

        foreach (var user in userList)
        {
            var existingUser = await dataService.GetAuthorizedApiUser(user.Key);
            if (!string.IsNullOrWhiteSpace(existingUser)) continue;
            
            var keyParts = user.Key.Split(".").Select(ss => ss.Trim()).ToArray();
            if (keyParts.Length != 2 || keyParts[0].Length != 7) throw new ArgumentException();
            await dataService.CreateUser(user.User, user.Key, keyParts[0]);
        }
    }
}