using LearnFlowERP.Application.Common.Interfaces;
using MediatR;

namespace LearnFlowERP.Application.Features.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommandHandler
        : IRequestHandler<DeleteStudentCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public DeleteStudentCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            DeleteStudentCommand request,
            CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .FindAsync(request.StudentId);

            if (student == null)
                throw new Exception("Student not found");

            student.IsActive = false;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}