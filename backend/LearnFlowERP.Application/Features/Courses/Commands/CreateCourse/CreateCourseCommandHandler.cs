using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler
        : IRequestHandler<CreateCourseCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateCourseCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<long> Handle(
            CreateCourseCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _context.Courses
                .AnyAsync(
                    x => x.CourseCode == request.CourseCode,
                    cancellationToken);

            if (exists)
                throw new Exception("Course code already exists");

            var course = new Course
            {
                CourseCode = request.CourseCode,
                CourseName = request.CourseName,
                Credits = request.Credits,

                TenantId = _currentUser.TenantId ?? 0,
                CreatedBy = _currentUser.UserId
            };

            _context.Courses.Add(course);

            await _context.SaveChangesAsync(cancellationToken);

            return course.CourseId;
        }
    }
}