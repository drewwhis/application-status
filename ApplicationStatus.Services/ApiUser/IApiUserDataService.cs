namespace ApplicationStatus.Services.ApiUser;

public interface IApiUserDataService
{
    Task<string?> GetAuthorizedApiUser(string apiKey);
}