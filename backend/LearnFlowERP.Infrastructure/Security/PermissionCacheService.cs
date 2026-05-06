using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace LearnFlowERP.Infrastructure.Security
{
    public class PermissionCacheService : IPermissionCacheService
    {
        private readonly IMemoryCache _cache;
        public PermissionCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<List<string>> GetPermissionsAsync(User user)
        {
            var cacheKey = $"perm_{user.UserId}";

            if (_cache.TryGetValue(cacheKey, out List<string> permissions))
                return Task.FromResult(permissions);

            permissions = user.UserRoles
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.Name)
                .Distinct()
                .ToList();

            _cache.Set(cacheKey, permissions, TimeSpan.FromMinutes(30));

            return Task.FromResult(permissions);
        }
    }
}
