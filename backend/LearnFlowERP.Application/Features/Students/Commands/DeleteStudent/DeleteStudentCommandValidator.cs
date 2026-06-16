using FluentValidation;

namespace LearnFlowERP.Application.Features.Students.Commands.DeleteStudent;

public class DeleteStudentCommandValidator
    : AbstractValidator<DeleteStudentCommand>
{
    public DeleteStudentCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0);
    }
}