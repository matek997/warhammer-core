
using WarhammerCore.WebApi.Models.Request;

namespace WarhammerCore.WebApi.Validation
{

    public class LoginRequestValidator : BaseAbstractValidator<LoginRequest>
    {
        /// <summary>
        /// Parameter cannot be null or empty.
        /// </summary>
        public LoginRequestValidator()
        {
            RuleForEmptyParameter(x => x.Email);
            RuleForEmptyParameter(x => x.Password);
        }
    }
}