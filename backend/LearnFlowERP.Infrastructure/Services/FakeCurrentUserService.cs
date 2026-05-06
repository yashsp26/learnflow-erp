using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnFlowERP.Application.Common.Interfaces;

namespace LearnFlowERP.Infrastructure.Services
{
    public class FakeCurrentUserService : ICurrentUserService
    {
        public long? UserId => 1;
        public long? TenantId => 1;
    }
}
