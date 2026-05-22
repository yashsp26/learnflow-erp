using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Application.Features.Users.Commands.Events;
using LearnFlowERP.Domain.Entities;
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
                UserType = request.UserType,
                ProfileCompleted = false,
            };

            user.UserRoles.Add(new UserRole
            {
                RoleId = request.RoleId,
                TenantId = tenantId
            });


            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);
            await _emailService.SendAsync(
                user.Email,
                "Your ERP Account",
                $"""
                Your account has been created.
            
                Email: {user.Email}
                Password: {tempPassword}
            
                Please login and complete your profile.
                """
            );

            await _mediator.Publish(
                new UserCreatedEvent(user.Email, user.Username),
                cancellationToken);

            return user.UserId;
        }

    }
}
