using Image.Common.Log;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Image.Server.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILog _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILog logger)
        {
            this._next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.Warning("The response has already started, the error handle middleware will not be executed.");
                    throw;
                }
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
            else if (exception is Exception) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { code = (int)code, error = exception.Message });
            _logger.Error(result); // record log
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }
    }

    public static class GlobErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalErrorHandlingMiddleware>();
        }
    }

}
