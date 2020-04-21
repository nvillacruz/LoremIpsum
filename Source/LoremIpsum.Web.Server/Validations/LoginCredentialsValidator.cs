using FluentValidation;
using LoremIpsum.Core;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// Validates the value of <see cref="LoginCredentialsApiModel"/>
    /// </summary>
    public class LoginCredentialsValidator : AbstractValidator<LoginCredentialsApiModel>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public LoginCredentialsValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                    .WithMessage("Username or Email must not be empty")
                .EmailAddress().When(x => x.UsernameOrEmail.Contains("@"), ApplyConditionTo.CurrentValidator)
                    .WithMessage("Invalid Email");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                    .WithMessage("Password must not be empty")
                .MinimumLength(8)
                    .WithMessage("Password must be greater than or equal to 8 characters");
        }

    }

}
