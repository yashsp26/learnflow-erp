using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Common.Models;
using LearnFlowERP.Application.Features.Courses.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Courses.Queries.GetCourses
{
    public class GetCoursesQueryHandler
        : IRequestHandler<GetCoursesQuery, PagedResult<CourseDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetCoursesQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<CourseDto>> Handle(
            GetCoursesQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.Courses
                .Where(x => x.IsActive)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x =>
                    x.CourseName.Contains(request.Search) ||
                    x.CourseCode.Contains(request.Search));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var courses = await query
                .OrderBy(x => x.CourseId)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CourseDto
                {
                    CourseId = x.CourseId,
                    CourseCode = x.CourseCode,
                    CourseName = x.CourseName,
                    Credits = x.Credits
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<CourseDto>
            {
                Items = courses,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(
                    totalCount / (double)request.PageSize)
            };
        }
    }
}