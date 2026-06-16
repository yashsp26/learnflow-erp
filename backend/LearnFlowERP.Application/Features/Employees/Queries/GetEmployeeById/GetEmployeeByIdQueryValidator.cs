using FluentValidation;

namespace LearnFlowERP.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryValidator
    : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
    {
        RuleFor(x => x.EmployeeId)
            .GreaterThan(0);
    }
}