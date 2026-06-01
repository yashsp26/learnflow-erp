using MediatR;

namespace LearnFlowERP.Application.Features.Employees.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand(long EmployeeId)
        : IRequest<Unit>;
}