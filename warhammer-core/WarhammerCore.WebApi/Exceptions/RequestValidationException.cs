using System;
using System.Collections.Generic;
using System.Linq;
using WarhammerCore.WebApi.Models.Errors;

namespace WarhammerCore.WebApi.Middleware
{
    /// <summary>
    /// Fluent validation error. Request parameter was invalid.
    /// </summary>
    public class RequestValidationException : Exception
    {
        /// <summary>
        /// List of properties that were invalid.
        /// </summary>
        public List<RequestValidationError> Errors { get; }

        public RequestValidationException(IEnumerable<RequestValidationError> errors)
        {
            Errors = errors?.ToList() ?? throw new ArgumentNullException(nameof(errors));
        }
    }
}