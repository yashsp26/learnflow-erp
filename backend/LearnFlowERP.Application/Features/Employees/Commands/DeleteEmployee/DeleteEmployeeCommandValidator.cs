using FluentValidation;

namespace LearnFlowERP.Application.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandValidator
    : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0);
    }
}