using System.Collections.Generic;
using WarhammerCore.WebApi.Models.Enums;

namespace WarhammerCore.WebApi.Models.Response
{
    public class ErrorResponse
    {
        public string ErrorCode { get; }
        public string Description { get; }

        /// <summary>
        /// Helpful information for the developer. Not meant to be shown on the production.
        /// </summary>
        public Dictionary<string, object> Data { get; }

        public ErrorResponse(string errorCode) : this(errorCode, null)
        {
        }

        public ErrorResponse(ErrorCode errorCode) : this(errorCode.ToString(), null)
        {
        }

        public ErrorResponse(string errorCode, string description)
        {
            ErrorCode = errorCode;
            Description = description;
            Data = new Dictionary<string, object>();
        }

        /// <summary>
        /// Add information for the developers. Don't show this on the production.
        /// </summary>
        public ErrorResponse AddData(string key, object value)
        {
            Data.Add(key, value);
            return this;
        }
    }
}