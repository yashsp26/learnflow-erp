using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace LearnFlowERP.Application.Features.Students.Commands.UpdateMyStudentProfile
{
    public class UpdateMyStudentProfileCommandHandler
        : IRequestHandler<UpdateMyStudentProfileCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public UpdateMyStudentProfileCommandHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(
            UpdateMyStudentProfileCommand request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            var student = await _context.Students
                .FirstOrDefaultAsync(
                    x => x.UserId == userId,
                    cancellationToken);

            if (student == null)
                throw new Exception("Student profile not found");

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Dob = request.Dob;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
