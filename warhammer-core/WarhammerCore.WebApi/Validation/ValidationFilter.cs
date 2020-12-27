using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarhammerCore.WebApi.Middleware;
using WarhammerCore.WebApi.Models.Errors;

namespace WarhammerCore.WebApi.Validation
{
    /// <summary>
    /// Validation filter that is set in <see cref="Startup"/> to be the first one ran (for fluent validation).
    /// </summary>
    public class ValidationFilter : IAsyncActionFilter
    {
        /// <summary>
        /// Ran before action is passed to the controller. Check for request validation errors (fluent validation).
        /// </summary>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var invalidProperties = context.ModelState.Where(x => x.Value.Errors.Count > 0);

                IEnumerable<RequestValidationError> errors = invalidProperties.Select(x => new RequestValidationError(x.Key, x.Value.Errors.Select(y => y.ErrorMessage)));

                throw new RequestValidationException(errors);
            }

            await next();
        }
    }
}
