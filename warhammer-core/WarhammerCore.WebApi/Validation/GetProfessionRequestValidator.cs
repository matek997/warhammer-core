using WarhammerCore.WebApi.Models.Request;

namespace WarhammerCore.WebApi.Validation
{
    /// <summary>
    /// Validate request model for the Profession/GetProfession in <see cref="ProfessionController"/>
    /// </summary>
    public class GetProfessionRequestValidator : BaseAbstractValidator<GetProfessionRequest>
    {
        /// <summary>
        /// Parameter cannot be null or empty.
        /// </summary>
        public GetProfessionRequestValidator()
        {
            RuleForEmptyParameter(x => x.ProfessionId);
        }
    }
}