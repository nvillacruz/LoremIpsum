using FluentValidation;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// Validators for <see cref="EnterpriseSettingsViewModel"/>
    /// </summary>
    public class EnterpriseSettingsViewModelValidator : AbstractValidator<EnterpriseSettingsViewModel>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public EnterpriseSettingsViewModelValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                    .WithMessage("Company is required!");
        }
    }
}
