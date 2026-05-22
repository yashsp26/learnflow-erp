using MediatR;

namespace LearnFlowERP.Application.Features.Onboarding.Students.Commands.CompleteStudentProfile
{
    public record CompleteStudentProfileCommand(
        string EnrollmentNo,
        string FirstName,
        string LastName,
        DateTime Dob
    ) : IRequest<Unit>;
}