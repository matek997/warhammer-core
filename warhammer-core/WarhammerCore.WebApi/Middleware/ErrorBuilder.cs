using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WarhammerCore.Abstract.Models;
using WarhammerCore.WebApi.Models.Response;

namespace WarhammerCore.WebApi.Middleware
{
    /// <summary>
    /// Error builder for the responses. Can be chained.
    /// </summary>
    public class ErrorBuilder
    {
        private readonly Exception exception;
        private readonly HttpContext context;
        private readonly Dictionary<string, object> data;
        private string errorCode, description;
        private int httpCode;
        private readonly AppSettings appSettings;

        public ErrorBuilder(HttpContext context, string requestBody, Exception ex, AppSettings appSettings)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.appSettings = appSettings;
            exception = ex;
            data = new Dictionary<string, object>();
            httpCode = StatusCodes.Status500InternalServerError;
            errorCode = "InternalServerError";

            if (!string.IsNullOrEmpty(requestBody)) AddData("RequestBody", requestBody);
        }

        /// <summary>
        /// Build response and send it.
        /// </summary>
        /// <returns></returns>
        public async Task BuildAndSendAsync()
        {
            string result = Build();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = httpCode;

            await context.Response.WriteAsync(result);
        }

        /// <summary>
        /// Build response model with the exception information.
        /// </summary>
        public string Build()
        {
            ErrorResponse response;

            if (appSettings.IsDevelopmentModeOn)
            {
                response = new ErrorResponse(errorCode, $"Description: {description}. Exception message: {exception.Message}");

                foreach (var point in data)
                    response.AddData(point.Key, point.Value);
            }
            else
            {
                response = new ErrorResponse(errorCode, description);
            }
            return JsonConvert.SerializeObject(response);
        }

        #region Chain Methods

        /// <summary>
        /// Set HTTP status code for response.
        /// </summary>
        /// <param name="httpCode">HTTP status code.</param>
        /// <returns>Error builder instance so we can chain methods.</returns>
        public ErrorBuilder SetHttpCode(int httpCode)
        {
            this.httpCode = httpCode;
            return this;
        }

        /// <summary>
        /// Set HTTP status code for response.
        /// </summary>
        /// <param name="httpCode">HTTP status code.</param>
        /// <returns>Error builder instance so we can chain methods.</returns>
        public ErrorBuilder SetHttpCode(HttpStatusCode httpCode) => SetHttpCode((int)httpCode);

        /// <summary>
        /// Set error code value.
        /// </summary>
        /// <param name="errorCode">Error code (enum or string value).</param>
        /// <returns>Error builder instance so we can chain methods.</returns>
        public ErrorBuilder SetErrorCode(object errorCode)
        {
            this.errorCode = errorCode.ToString();
            return this;
        }

        /// <summary>
        /// Set custom description.
        /// </summary>
        /// <param name="description"></param>
        /// <returns>Error builder instance so we can chain methods.</returns>
        public ErrorBuilder SetDescription(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// Add information to data list.
        /// </summary>
        /// <param name="key">Data key.</param>
        /// <param name="value">Data value.</param>
        /// <returns>Error builder instance so we can chain methods.</returns>
        public ErrorBuilder AddData(string key, object value)
        {
            data.Add(key, value);
            return this;
        }

        #endregion Chain Methods
    }
}