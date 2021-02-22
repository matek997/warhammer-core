using WarhammerCore.WebApi.Models.Request;

namespace WarhammerCore.WebApi.Validation
{
    /// <summary>
    /// Validate request model for the Profession/GetProfession in <see cref="ProfessionController"/>
    /// </summary>
    public class CreateProfessionRequestValidator : BaseAbstractValidator<CreateProfessionRequest>
    {
        /// <summary>
        /// Parameter cannot be null or empty.
        /// </summary>
        public CreateProfessionRequestValidator()
        {
            RuleForEmptyParameter(x => x.Id);
            RuleForEmptyParameter(x => x.IsAdvanced);
            RuleForEmptyParameter(x => x.Label);
            RuleForEmptyParameter(x => x.Notes);
            RuleForEmptyParameter(x => x.Role);
            RuleForEmptyParameter(x => x.Source);
            RuleForEmptyParameter(x => x.Description);
            RuleForEmptyParameter(x => x.MainProfile);
            RuleForEmptyParameter(x => x.SecondaryProfile);
        }
    }
}