using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LearnFlowERP.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LearnFlowERP.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long? UserId =>
            long.TryParse(
                _httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId"),
                out var id) ? id : null;

        public long? TenantId =>
            long.TryParse(
                _httpContextAccessor.HttpContext?.User?.FindFirstValue("TenantId"),
                out var id) ? id : null;
    }
}
