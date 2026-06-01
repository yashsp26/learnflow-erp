using LearnFlowERP.Application.Features.Employees.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Employees.Queries.GetMyEmployeeProfile
{
    public record GetMyEmployeeProfileQuery()
        : IRequest<EmployeeDto>;
}