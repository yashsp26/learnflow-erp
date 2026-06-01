using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Students.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQueryHandler
        : IRequestHandler<GetStudentByIdQuery, StudentDto>
    {
        private readonly IApplicationDbContext _context;

        public GetStudentByIdQueryHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StudentDto> Handle(
            GetStudentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var student = await _context.Students
                .Include(x => x.User)
                .FirstOrDefaultAsync(
                    x => x.StudentId == request.StudentId && x.IsActive,
                    cancellationToken);

            if (student == null)
                throw new Exception("Student not found");

            return new StudentDto
            {
                StudentId = student.StudentId,
                UserId = student.UserId,
                EnrollmentNo = student.EnrollmentNo,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Dob = student.Dob,
                Email = student.User!.Email,
                DocumentUrl = student.DocumentUrl
            };
        }
    }
}