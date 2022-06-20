using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace Swap.GithubTracker.Services.Api.Extensions
{
    public static class ErrorHandlerExtensions
    {
        public static IApplicationBuilder UseErrorHandler(
                                          this IApplicationBuilder appBuilder,
                                          ILoggerFactory loggerFactory)
        {
            return appBuilder.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context
                                                    .Features
                                                    .Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {

                        var logger = loggerFactory.CreateLogger("ErrorHandler");
                        logger.LogError($"Error: {exceptionHandlerFeature.Error}");

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var json = new
                        {
                            context.Response.StatusCode,
                            Message = "Internal Server Error",
                        };

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(json));
                    }
                });
            });
        }
    }

}
