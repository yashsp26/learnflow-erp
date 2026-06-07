using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Users.Commands.UpdateUserAvatar;
using MediatR;

namespace LearnFlowERP.Application.Features.Students.Commands.UpdateStudentDocument
{
    public class UpdateStudentDocumentCommandHandler
    : IRequestHandler<UpdateStudentDocumentCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStudentDocumentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            UpdateStudentDocumentCommand request,
            CancellationToken cancellationToken)
        {
            var student = await _context.Students.FindAsync(request.StudentId);

            if (student == null)
                throw new NotFoundException("Student not found");

            student.DocumentUrl = request.DocumentUrl;

            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
