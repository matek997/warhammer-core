using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Exceptions;
using WarhammerCore.Abstract.Models;
using WarhammerCore.WebApi.Exceptions;
using WarhammerCore.WebApi.Middleware;

namespace HostApp.WebApi.Middlewares
{
    /// <summary>
    /// Middleware that catches the errors and returns a commonly structed error.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Regular invoke method.
        /// Read the request body (request body is a stream).
        /// </summary>
        public async Task Invoke(HttpContext context, AppSettings appSettings, ILogger<ErrorHandlingMiddleware> logger)
        {
            string requestBody = string.Empty;

            try
            {
                context.Request.EnableBuffering();
                requestBody = await ReadRequestBodyAsync(context);
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                logger.LogError(ex.StackTrace);
                await HandleExceptionAsync(context, requestBody, ex, appSettings);
            }
        }

        /// <summary>
        /// Read request body stream.
        /// </summary>
        private static async Task<string> ReadRequestBodyAsync(HttpContext context)
        {
            string requestBody = string.Empty;

            if (context.Request.Body?.CanRead == true)
            {
                var reader = await context.Request.BodyReader.ReadAsync();
                context.Request.Body.Position = 0;
                var buffer = reader.Buffer;
                requestBody = Encoding.UTF8.GetString(buffer.FirstSpan);
                context.Request.Body.Position = 0;
            }

            return requestBody;
        }

        /// <summary>
        /// Handle exception and build response for it.
        /// </summary>
        /// <param name="context">HTTP context.</param>
        /// <param name="requestBody">Request body content.</param>
        /// <param name="ex">Exception information.</param>
        private static Task HandleExceptionAsync(HttpContext context, string requestBody, Exception ex, AppSettings appSettings)
        {
            ErrorBuilder errorBuilder = new ErrorBuilder(context, requestBody, ex, appSettings);

            if (ex is ControllerException controllerEx) return BuildAndSendAsync(controllerEx, errorBuilder);
            if (ex is RequestValidationException validationEx) return BuildAndSendAsync(validationEx, errorBuilder);
            if (ex is AppBusinessException businessEx) return BuildAndSendAsync(businessEx, errorBuilder);

            return errorBuilder.BuildAndSendAsync();
        }

        /// <summary>
        /// Build response for <see cref="ControllerException"/>.
        /// </summary>
        /// <param name="ex">Exception information about an error that happened in the WebApi project.</param>
        private static Task BuildAndSendAsync(ControllerException ex, ErrorBuilder errorBuilder)
        {
            return errorBuilder
                    .SetHttpCode(ex.StatusCode)
                    .SetErrorCode(ex.ErrorCode)
                    .BuildAndSendAsync();
        }

        /// <summary>
        /// Build response for <see cref="AppBusinessException"/>.
        /// </summary>
        /// <param name="ex">Exception information about an error that happened in the Business part of the project.</param>
        private static Task BuildAndSendAsync(AppBusinessException ex, ErrorBuilder errorBuilder)
        {
            return errorBuilder
                    .SetErrorCode(ex.ErrorCode)
                    .SetDescription(ex.Message)
                    .BuildAndSendAsync();
        }

        /// <summary>
        /// Build response for <see cref="RequestValidationException"/>.
        /// </summary>
        /// <param name="ex">Exception information about an error that happened in the WebApi project.</param>
        private static Task BuildAndSendAsync(RequestValidationException ex, ErrorBuilder errorBuilder)
        {
            foreach (var error in ex.Errors)
                errorBuilder.AddData($"Invalid property: {error.PropertyName}", string.Join("; ", error.Messages));

            return errorBuilder
                .SetDescription("Invalid request parameters")
                .SetHttpCode(HttpStatusCode.BadRequest)
                .SetErrorCode("RequestValidationError")
                .BuildAndSendAsync();
        }
    }

    /// <summary>
    /// Error handling extension.
    /// </summary>
    public static class ErrorHandlingMiddlewareExtension
    {
        /// <summary>
        /// Use error handling extension, to provide JSON response in case of errors.
        /// </summary>
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            return app;
        }
    }
}