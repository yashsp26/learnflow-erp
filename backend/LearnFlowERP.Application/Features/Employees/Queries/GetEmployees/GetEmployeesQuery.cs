using LearnFlowERP.Application.Common.Models;
using LearnFlowERP.Application.Features.Employees.DTOs;
using MediatR;

namespace LearnFlowERP.Application.Features.Employees.Queries.GetEmployees
{
    public class GetEmployeesQuery
        : IRequest<PagedResult<EmployeeDto>>
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }
    }
}