using MediatR;

namespace LearnFlowERP.Application.Features.Onboarding.Students.Commands.CompleteStudentProfile
{
    public class CompleteStudentProfileCommand : IRequest<Unit>
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime Dob { get; set; }
    }
}