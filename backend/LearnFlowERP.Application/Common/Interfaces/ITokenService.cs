using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnFlowERP.Domain.Entities;

namespace LearnFlowERP.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(User user);
        string GenerateRefreshToken();
    }
}
