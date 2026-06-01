using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Common.Models;
using LearnFlowERP.Application.Features.Employees.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Employees.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler
        : IRequestHandler<GetEmployeesQuery, PagedResult<EmployeeDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetEmployeesQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<EmployeeDto>> Handle(
            GetEmployeesQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.Employees
                .Include(x => x.User)
                .Where(x => x.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x =>
                    (x.FirstName ?? "").Contains(request.Search) ||
                    (x.LastName ?? "").Contains(request.Search) ||
                    x.EmpCode.Contains(request.Search));
            }

            var totalCount =
                await query.CountAsync(cancellationToken);

            var employees = await query
                .OrderBy(x => x.EmployeeId)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new EmployeeDto
                {
                    EmployeeId = x.EmployeeId,
                    UserId = x.UserId,
                    EmpCode = x.EmpCode,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Department = x.Department,
                    Salary = x.Salary,
                    Email = x.User!.Email,
                    DocumentUrl = x.DocumentUrl
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<EmployeeDto>
            {
                Items = employees,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages =
                    (int)Math.Ceiling(
                        totalCount /
                        (double)request.PageSize)
            };
        }
    }
}