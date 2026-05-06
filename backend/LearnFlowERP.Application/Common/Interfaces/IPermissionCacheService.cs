using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnFlowERP.Domain.Entities;

namespace LearnFlowERP.Application.Common.Interfaces
{
    public interface IPermissionCacheService
    {
        Task<List<string>> GetPermissionsAsync(User user);
    }
}
