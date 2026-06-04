using LearnFlowERP.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LearnFlowERP.Api.Authorization
{
    public class PermissionHandler
        : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionCacheService _permissionCache;

        public PermissionHandler(
            IPermissionCacheService permissionCache)
        {
            _permissionCache = permissionCache;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var userIdClaim =
                context.User.FindFirst("UserId");

            if (userIdClaim == null)
                return;

            var userId =
                long.Parse(userIdClaim.Value);

            var permissions =
                await _permissionCache
                    .GetPermissionsAsync(userId);

            if (permissions.Contains(
                requirement.Permission,
                StringComparer.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
            }
        }
    }
}