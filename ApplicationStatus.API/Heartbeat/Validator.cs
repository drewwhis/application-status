using ApplicationStatus.DTO.Heartbeat;
using FluentValidation;

namespace ApplicationStatus.API.Heartbeat;

public class Validator : AbstractValidator<Input>
{
    public Validator()
    {
        RuleFor(x => x.ApplicationName).NotEmpty().MinimumLength(2);
    }
}