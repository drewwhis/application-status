using FluentValidation;

namespace ApplicationStatus.API.Heartbeat;

public class HeartbeatValidator : AbstractValidator<Models.Heartbeat>
{
    public HeartbeatValidator()
    {
        RuleFor(x => x.ApplicationName).NotEmpty().MinimumLength(2);
    }
}