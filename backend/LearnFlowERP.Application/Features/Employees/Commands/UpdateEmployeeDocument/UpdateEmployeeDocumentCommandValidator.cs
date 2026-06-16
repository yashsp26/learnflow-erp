using FluentValidation;

namespace LearnFlowERP.Application.Features.Employees.Commands.UpdateEmployeeDocument;

public class UpdateEmployeeDocumentCommandValidator
    : AbstractValidator<UpdateEmployeeDocumentCommand>
{
    public UpdateEmployeeDocumentCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0);

        RuleFor(x => x.DocumentUrl)
            .NotEmpty()
            .MaximumLength(500);
    }
}