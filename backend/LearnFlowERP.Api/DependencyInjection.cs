using LearnFlowERP.Application;
using LearnFlowERP.Infrastructure;

namespace LearnFlowERP.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            services.AddApplicationDI();
                services.AddInfrastructureDI();
            return services;
        }
    }
}
