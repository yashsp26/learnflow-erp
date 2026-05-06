using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnFlowERP.Application;
using Microsoft.Extensions.DependencyInjection;

namespace LearnFlowERP.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddApplicationDI();
            return services;
        }
    }
}
