using FluentValidation;

namespace LearnFlowERP.Application.Features.Auth.Commands.RefreshTokenCommands;

public class RefreshTokenCommandValidator
    : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .MinimumLength(20);
    }
}