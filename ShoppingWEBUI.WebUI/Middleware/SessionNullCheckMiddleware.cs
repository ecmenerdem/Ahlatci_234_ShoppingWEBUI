using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ShoppingWEBUI.Helper.SessionHelper;
using System.Threading.Tasks;

namespace ShoppingWEBUI.WebUI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SessionNullCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionNullCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            if (httpContext.Request.Path.Value.Contains("/Admin/")) 
            {
                if (SessionManager.LoggedUser==null)
                {
                    httpContext.Response.Redirect("/AdminAccount/Login");
                    httpContext.Response.WriteAsync("Yetksiz Giriş");
                }
            }

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SessionNullCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseSessionNullCheckMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SessionNullCheckMiddleware>();
        }
    }
}
