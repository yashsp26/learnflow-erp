using LearnFlowERP.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LearnFlowERP.Infrastructure.Security
{
    public class PermissionCacheService
        : IPermissionCacheService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        public PermissionCacheService(
            IApplicationDbContext context,
            IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<string>> GetPermissionsAsync(long userId)
        {
            var cacheKey = $"user_permissions_{userId}";

            if (_cache.TryGetValue(
                cacheKey,
                out List<string>? permissions))
            {
                return permissions!;
            }

            var user = await _context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.RolePermissions)
                            .ThenInclude(x => x.Permission)

                .Include(x => x.Employee)
                    .ThenInclude(x => x.Designation)
                        .ThenInclude(x => x.DesignationPermissions)
                            .ThenInclude(x => x.Permission)

                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
                return new List<string>();

            var permissionSet = new HashSet<string>(
                StringComparer.OrdinalIgnoreCase);

            // Role Permissions
            permissionSet.UnionWith(
                user.UserRoles
                    .SelectMany(x => x.Role.RolePermissions)
                    .Select(x => x.Permission.Name));

            // Designation Permissions
            if (user.Employee?.Designation != null)
            {
                permissionSet.UnionWith(
                    user.Employee.Designation
                        .DesignationPermissions
                        .Select(x => x.Permission.Name));
            }

            permissions = permissionSet.ToList();

            _cache.Set(
                cacheKey,
                permissions,
                TimeSpan.FromHours(1));

            return permissions;
        }

        public void RemoveUserPermissions(long userId)
        {
            _cache.Remove($"user_permissions_{userId}");
        }



        // To remove permissions immeadiately after someone revokes them because they may be in cache
        
        public async Task RemoveDesignationUsersPermissionsAsync(
    long designationId)
        {
            var userIds = await _context.Employees
                .Where(x => x.DesignationId == designationId)
                .Select(x => x.UserId)
                .ToListAsync();

            foreach (var userId in userIds)
            {
                if (userId.HasValue)
                {
                    _cache.Remove(
                        $"user_permissions_{userId.Value}");
                }
            }
        }

        public async Task RemoveRoleUsersPermissionsAsync(
    long roleId)
        {
            var userIds = await _context.UserRoles
                .Where(x => x.RoleId == roleId)
                .Select(x => x.UserId)
                .Distinct()
                .ToListAsync();

            foreach (var userId in userIds)
            {
                _cache.Remove(
                    $"user_permissions_{userId}");
            }
        }
    }
}