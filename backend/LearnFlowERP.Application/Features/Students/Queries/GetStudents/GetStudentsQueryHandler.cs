using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Common.Models;
using LearnFlowERP.Application.Features.Students.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Students.Queries.GetStudents
{
    public class GetStudentsQueryHandler
        : IRequestHandler<GetStudentsQuery, PagedResult<StudentDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentsQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<StudentDto>> Handle(
            GetStudentsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.Students
                .Include(x => x.User)
                .Where(x => x.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x =>
                    (x.FirstName ?? "").Contains(request.Search) ||
                    (x.LastName ?? "").Contains(request.Search) ||
                    x.EnrollmentNo.Contains(request.Search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var students = await query
                .OrderBy(x => x.StudentId)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new StudentDto
                {
                    StudentId = x.StudentId,
                    UserId = x.UserId,
                    EnrollmentNo = x.EnrollmentNo,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Dob = x.Dob,
                    Email = x.User!.Email,
                    DocumentUrl = x.DocumentUrl
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<StudentDto>
            {
                Items = students,
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