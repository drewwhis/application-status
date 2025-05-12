namespace ApplicationStatus.Services.ApiUser;

public interface IApiUserDataService
{
    Task<string?> GetAuthorizedApiUser(string apiKey);
    Task<Models.ApiUser?> CreateUser(string applicationName, string apiKey, string prefix);
}