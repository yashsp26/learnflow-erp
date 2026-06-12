using LearnFlowERP.Application.Common.Interfaces;
using MediatR;

namespace LearnFlowERP.Application.Features.Permissions.Queries.GetCurrentUserPermissions
{
    public class GetCurrentUserPermissionsQueryHandler
        : IRequestHandler<
            GetCurrentUserPermissionsQuery,
            List<string>>
    {
        private readonly IPermissionCacheService _permissionCache;
        private readonly ICurrentUserService _currentUser;

        public GetCurrentUserPermissionsQueryHandler(
            IPermissionCacheService permissionCache,
            ICurrentUserService currentUser)
        {
            _permissionCache = permissionCache;
            _currentUser = currentUser;
        }

        public async Task<List<string>> Handle(
            GetCurrentUserPermissionsQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            if (!userId.HasValue)
            {
                return new List<string>();
            }

            return await _permissionCache
                .GetPermissionsAsync(
                    userId.Value,
                    cancellationToken);
        }
    }
}