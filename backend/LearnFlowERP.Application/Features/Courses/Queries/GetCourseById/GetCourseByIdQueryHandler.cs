using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Courses.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQueryHandler
        : IRequestHandler<GetCourseByIdQuery, CourseDto>
    {
        private readonly IApplicationDbContext _context;

        public GetCourseByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CourseDto> Handle(
            GetCourseByIdQuery request,
            CancellationToken cancellationToken)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(
                    x => x.CourseId == request.CourseId &&
                         x.IsActive,
                    cancellationToken);

            if (course == null)
                throw new Exception("Course not found");

            return new CourseDto
            {
                CourseId = course.CourseId,
                CourseCode = course.CourseCode,
                CourseName = course.CourseName,
                Credits = course.Credits
            };
        }
    }
}