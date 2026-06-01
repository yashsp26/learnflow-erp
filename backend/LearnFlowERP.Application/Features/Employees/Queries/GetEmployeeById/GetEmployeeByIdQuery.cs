using LearnFlowERP.Application.Features.Employees.DTOs;
using MediatR;


namespace LearnFlowERP.Application.Features.Employees.Queries.GetEmployeeById
{
    public record GetEmployeeByIdQuery(long EmployeeId)
    : IRequest<EmployeeDto>;
}
