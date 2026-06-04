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

        public async Task<List<string>> GetPermissionsAsync(long roleId)
        {
            var cacheKey = $"role_permissions_{roleId}";

            if (_cache.TryGetValue(cacheKey,
                out List<string>? permissions))
            {
                return permissions!;
            }

            permissions = await _context.RolePermissions
                .Where(x => x.RoleId == roleId)
                .Include(x => x.Permission)
                .Select(x => x.Permission.Name)
                .ToListAsync();

            _cache.Set(
                cacheKey,
                permissions,
                TimeSpan.FromHours(1));

            return permissions;
        }

        public void RemoveRolePermissions(long roleId)
        {
            _cache.Remove($"role_permissions_{roleId}");
        }
    }
}