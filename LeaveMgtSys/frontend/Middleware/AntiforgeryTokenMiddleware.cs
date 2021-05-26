using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace frontend.Middleware
{
    public class AntiforgeryTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAntiforgery _antiforgery;

        public AntiforgeryTokenMiddleware(RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;
        }

        public Task Invoke(HttpContext context)
        {
            var cookieOptions = new CookieOptions()
            {
                Path = "/",
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddMinutes(60),
                IsEssential = true
                // Expires = DateTime.Now.AddMonths(1),
            };

            //if (context.Request.Method.ToLower() == "post")
            //{
            if (Convert.ToString(context.Request.Path).Contains("/"))
            {
                var tokens = _antiforgery.GetAndStoreTokens(context);

                context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, cookieOptions);
            }
            //}
            return _next(context);
        }


    }

    public static class AntiforgeryTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseAntiforgeryToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AntiforgeryTokenMiddleware>();
        }
    }
}
