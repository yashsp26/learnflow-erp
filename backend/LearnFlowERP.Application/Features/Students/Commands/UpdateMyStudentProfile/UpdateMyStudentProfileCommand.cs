using MediatR;

namespace LearnFlowERP.Application.Features.Students.Commands.UpdateMyStudentProfile
{
    public class UpdateMyStudentProfileCommand : IRequest<Unit>
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime Dob { get; set; }
    }
}