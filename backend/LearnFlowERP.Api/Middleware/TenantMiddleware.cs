namespace LearnFlowERP.Api.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var tenantId = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();

            if (!string.IsNullOrEmpty(tenantId))
            {
                context.Items["TenantId"] = Convert.ToInt64(tenantId);
            }

            await _next(context);
        }
    }
}
