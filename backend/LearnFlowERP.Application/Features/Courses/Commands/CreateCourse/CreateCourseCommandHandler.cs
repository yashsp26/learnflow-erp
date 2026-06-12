using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler
        : IRequestHandler<CreateCourseCommand, string>
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

        public async Task<string> Handle(
            CreateCourseCommand request,
            CancellationToken cancellationToken)
        {
            var exists = await _context.Courses
     .FirstOrDefaultAsync(
         x =>
             x.CourseCode.ToLower() == request.CourseCode.ToLower() &&
             x.CourseName.ToLower() == request.CourseName.ToLower() &&
             x.Credits == request.Credits,
         cancellationToken);

            if (exists != null)
            {
                exists.IsActive = true;
                await _context.SaveChangesAsync(cancellationToken);

                return "Course already exists and has been reactivated.";
            }

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

            return "Course Added Successfully.";
        }
    }
}