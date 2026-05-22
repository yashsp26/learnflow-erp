using MediatR;

namespace LearnFlowERP.Application.Features.Onboarding.Commands.Employees.CompleteEmployeeProfile
{
    public record CompleteEmployeeProfileCommand(
        string EmpCode,
        string FirstName,
        string LastName,
        string Department
    ) : IRequest<Unit>;
}