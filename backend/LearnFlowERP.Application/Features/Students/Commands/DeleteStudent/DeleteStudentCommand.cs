using MediatR;

namespace LearnFlowERP.Application.Features.Students.Commands.DeleteStudent
{
    public record DeleteStudentCommand(long StudentId)
        : IRequest<Unit>;
}