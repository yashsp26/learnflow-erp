using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Onboarding.Students.Commands.CompleteStudentProfile;
using LearnFlowERP.Domain.Entities;
using LearnFlowERP.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Onboarding.Students.Commands.CompleteStudentProfile
{
    public class CompleteStudentProfileCommandHandler
        : IRequestHandler<CompleteStudentProfileCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CompleteStudentProfileCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            CompleteStudentProfileCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId
                ?? throw new UnauthorizedAccessException();

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
                throw new NotFoundException("User not found");

            if (user.UserType != UserType.Student)
                throw new InvalidOperationException("Invalid user type");

            var currentYear = DateTime.Now.Year;

            var lastStudent = await _context.Students
                .OrderByDescending(x => x.StudentId)
                .FirstOrDefaultAsync(cancellationToken);

            var nextNumber = (lastStudent?.StudentId ?? 0) + 1;

            var enrollmentNo = $"STU-{currentYear}-{nextNumber:D4}";


            var student = new Student
            {
                UserId = user.UserId,
                TenantId = user.TenantId,
                EnrollmentNo = enrollmentNo,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Dob = request.Dob
            };

            _context.Students.Add(student);

            user.ProfileCompleted = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}