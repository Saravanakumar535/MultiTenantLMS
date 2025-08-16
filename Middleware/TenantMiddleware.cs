using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MultiTenantLMS.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Example: Get tenant from custom header
            if (context.Request.Headers.TryGetValue("X-Tenant-Domain", out var tenantDomain))
            {
                context.Items["TenantDomain"] = tenantDomain.ToString();
            }
            else
            {
                context.Items["TenantDomain"] = "default"; // fallback
            }

            await _next(context);
        }
    }

    public static class TenantMiddlewareExtensions
    {
        public static IApplicationBuilder UseTenantMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}
