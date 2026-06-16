using FluentValidation;

namespace LearnFlowERP.Application.Features.Auth.Commands.LogOut;

public class LogoutCommandValidator
    : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty();
    }
}