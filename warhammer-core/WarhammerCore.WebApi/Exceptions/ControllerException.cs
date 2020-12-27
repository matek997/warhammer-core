using System;
using System.Net;
using WarhammerCore.WebApi.Models.Enums;

namespace WarhammerCore.WebApi.Exceptions
{
    /// <summary>
    /// Exception happened in the WebAPI controllers.
    /// </summary>
    public class ControllerException : Exception
    {
        public ErrorCode ErrorCode { get; }
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;

        public ControllerException(ErrorCode errorCode, string message, HttpStatusCode statusCode) : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }
}
