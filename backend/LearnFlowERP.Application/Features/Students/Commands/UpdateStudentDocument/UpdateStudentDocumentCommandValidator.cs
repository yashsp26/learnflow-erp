using FluentValidation;

namespace LearnFlowERP.Application.Features.Students.Commands.UpdateStudentDocument;

public class UpdateStudentDocumentCommandValidator
    : AbstractValidator<UpdateStudentDocumentCommand>
{
    public UpdateStudentDocumentCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0);

        RuleFor(x => x.DocumentUrl)
            .NotEmpty()
            .MaximumLength(500);
    }
}