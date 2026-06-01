using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Students.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Students.Queries.GetMyStudentProfile
{
    public class GetMyStudentProfileQueryHandler
        : IRequestHandler<GetMyStudentProfileQuery, StudentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetMyStudentProfileQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<StudentDto> Handle(
            GetMyStudentProfileQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var student = await _context.Students
                .Include(x => x.User)
                .FirstOrDefaultAsync(
                    x => x.UserId == userId,
                    cancellationToken);

            if (student == null)
                throw new Exception("Student profile not found");

            return new StudentDto
            {
                StudentId = student.StudentId,
                EnrollmentNo = student.EnrollmentNo,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Dob = student.Dob,
                DocumentUrl = student.DocumentUrl,
                Email = student.User!.Email
            };
        }
    }
}