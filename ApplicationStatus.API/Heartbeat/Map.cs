using ApplicationStatus.DTO.Heartbeat;

namespace ApplicationStatus.API.Heartbeat;

public static class Map
{
    public static Models.Heartbeat ToModel(this Input input)
    {
        return new Models.Heartbeat
        {
            Id = Guid.CreateVersion7(),
            ApplicationName = input.ApplicationName,
            IsGood = input.IsGood,
            LastUpdate = DateTime.Now
        };
    }

    public static Output ToOutput(this Models.Heartbeat output)
    {
        return new Output
        {
            ApplicationName = output.ApplicationName,
            IsGood = output.IsGood,
            LastUpdate = output.LastUpdate
        };
    }
}