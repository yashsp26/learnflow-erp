using FluentValidation;

namespace LearnFlowERP.Application.Features.Employees.Queries.GetEmployees;

public class GetEmployeesQueryValidator
    : AbstractValidator<GetEmployeesQuery>
{
    public GetEmployeesQueryValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100);
    }
}