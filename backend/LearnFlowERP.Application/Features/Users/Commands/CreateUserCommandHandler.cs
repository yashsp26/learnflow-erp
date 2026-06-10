using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Users.Commands.Events;
using LearnFlowERP.Domain.Entities;
using LearnFlowERP.Domain.Enums;
using MediatR;


namespace LearnFlowERP.Application.Features.Users.Commands
{
    public class CreateUserCommandHandler
    : IRequestHandler<CreateUserCommand, long>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _hasher;
        private readonly ICurrentUserService _currentUser;
        private readonly IEmailService _emailService;
        private readonly IMediator _mediator;
        public CreateUserCommandHandler(
            IApplicationDbContext context,
            IPasswordHasher hasher,
            ICurrentUserService currentUser,
            IEmailService emailService,
            IMediator mediator)
        {
            _context = context;
            _hasher = hasher;
            _currentUser = currentUser;
            _emailService = emailService;
            _mediator = mediator;
        }

        public async Task<long> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            var tempPassword = Guid.NewGuid()
                .ToString()[..4];
            var tenantId = _currentUser.TenantId
                ?? throw new UnauthorizedAccessException("Tenant not found");
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _hasher.Hash(tempPassword),
                TenantId = _currentUser.TenantId ?? 0,
                CreatedBy = _currentUser.UserId,
                UserType = Enum.IsDefined(typeof(UserType), (int)request.RoleId)
                    ? (UserType)request.RoleId
                    : throw new InvalidOperationException($"Invalid RoleId: {request.RoleId}"),
                ProfileCompleted = false,
            };

            user.UserRoles.Add(new UserRole
            {
                RoleId = request.RoleId,
                TenantId = tenantId
            });


            _context.Users.Add(user);

            try
            {
                await _emailService.SendAsync(
                    user.Email,
                    "Your ERP Account Has Been Created",
                    $"""
                <div style="font-family: Arial, sans-serif; line-height: 1.6;">
                    <h2 style="color:#2563eb;">Welcome to LearnFlow ERP</h2>
            
                    <p>Hello,</p>
            
                    <p>Your ERP account has been successfully created. 
                    You can use the credentials below to sign in:</p>
            
                    <table style="border-collapse: collapse; margin: 15px 0;">
                        <tr>
                            <td style="padding: 8px; font-weight: bold;">Email:</td>
                            <td style="padding: 8px;">{user.Email}</td>
                        </tr>
                        <tr>
                            <td style="padding: 8px; font-weight: bold;">Temporary Password:</td>
                            <td style="padding: 8px;">{tempPassword}</td>
                        </tr>
                    </table>
            
                    <p>
                        <strong>Important:</strong> For security reasons, 
                        please change your password 
                        after your first login and complete your profile information.
                    </p>
            
                    <p>Thank you,<br/>LearnFlow ERP Team</p>
                                    <p style="margin:20px 0;">
                </p>
                </div>
                """
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Email failed: {ex.Message}");
            }

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(
                new UserCreatedEvent(user.Email, user.Username),
                cancellationToken);

            return user.UserId;
        }

    }
}
