using LearnFlowERP.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LearnFlowERP.Application.Features.Notifications.Queries.GetUnreadNotificationCount
{
    public class GetUnreadNotificationCountQueryHandler
        : IRequestHandler<GetUnreadNotificationCountQuery, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetUnreadNotificationCountQueryHandler(
            IApplicationDbContext context,
            ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<int> Handle(
            GetUnreadNotificationCountQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId!.Value;

            return await _context.UserNotifications
                .CountAsync(
                    x => x.UserId == userId &&
                         !x.IsRead,
                    cancellationToken);
        }
    }
}