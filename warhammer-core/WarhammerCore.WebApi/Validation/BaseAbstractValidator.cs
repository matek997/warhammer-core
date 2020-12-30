using FluentValidation;
using System;
using System.Linq.Expressions;

namespace WarhammerCore.WebApi.Validation
{
    /// <summary>
    /// Base class for the validators. Contains common logic and common tests for the parameters validation.
    /// </summary>
    /// <typeparam name="T">Model class that will be validated.</typeparam>
    public abstract class BaseAbstractValidator<T> : AbstractValidator<T> where T : class
    {
        /// <summary>
        /// Create error messages based on the condition that has failed.
        /// </summary>
        /// <typeparam name="T2">Result of the <see cref="parameter"/> lambda expression.</typeparam>
        /// <param name="parameter">Lambda expression that has a test for the parameter.</param>
        /// <returns>String error message for the expression result.</returns>
        public IRuleBuilderOptions<T, T2> RuleForEmptyParameter<T2>(Expression<Func<T, T2>> parameter)
        {
            return RuleFor(parameter)
                .NotNull().WithMessage($"{nameof(parameter)} must have a value.")
                .NotEmpty().WithMessage($"{nameof(parameter)} must have a value.");
        }
    }
}
