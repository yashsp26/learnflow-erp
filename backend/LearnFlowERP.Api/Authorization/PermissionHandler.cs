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
            var roleIdClaim =
                context.User.FindFirst("RoleId");

            if (roleIdClaim == null)
                return;

            var roleId =
                long.Parse(roleIdClaim.Value);

            var permissions =
                await _permissionCache
                    .GetPermissionsAsync(roleId);

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}