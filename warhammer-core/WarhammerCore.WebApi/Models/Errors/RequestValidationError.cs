using System;
using System.Collections.Generic;
using System.Linq;

namespace WarhammerCore.WebApi.Models.Errors
{
    /// <summary>
    /// Fluent validation parameter error.
    /// </summary>
    public class RequestValidationError
    {
        public string PropertyName { get; }
        public List<string> Messages { get; }

        public RequestValidationError(string propertyName, IEnumerable<string> messages)
        {
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentException("Value cannot be null or empty", nameof(propertyName));

            PropertyName = propertyName;
            Messages = messages?.ToList() ?? throw new ArgumentNullException(nameof(messages));
        }
    }
}