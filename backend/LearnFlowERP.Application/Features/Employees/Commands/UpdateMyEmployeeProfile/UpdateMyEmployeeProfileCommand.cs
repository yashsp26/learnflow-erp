using MediatR;

namespace LearnFlowERP.Application.Features.Employees.Commands.UpdateMyEmployeeProfile
{
    public class UpdateMyEmployeeProfileCommand
        : IRequest<Unit>
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Department { get; set; } = null!;
    }
}