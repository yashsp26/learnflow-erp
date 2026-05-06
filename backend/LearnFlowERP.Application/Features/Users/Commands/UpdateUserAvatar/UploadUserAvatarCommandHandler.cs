using LearnFlowERP.Application.Common.Interfaces;
using MediatR;

namespace LearnFlowERP.Application.Features.Users.Commands.UpdateUserAvatar
{
    public class UpdateUserAvatarCommandHandler
        : IRequestHandler<UpdateUserAvatarCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateUserAvatarCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            UpdateUserAvatarCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);

            if (user == null)
                throw new Exception("User not found");

            user.ProfileImageUrl = request.AvatarUrl;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
