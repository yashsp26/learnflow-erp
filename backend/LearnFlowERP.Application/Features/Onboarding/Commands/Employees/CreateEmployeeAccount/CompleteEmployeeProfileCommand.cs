using MediatR;

namespace LearnFlowERP.Application.Features.Onboarding.Commands.Employees.CompleteEmployeeProfile
{
    public class CompleteEmployeeProfileCommand : IRequest<Unit>
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Department { get; set; } = null!;
    }
}