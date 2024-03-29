﻿using LoremIpsum.Core;
using FluentValidation;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// 
    /// </summary>
    public class ForgotPasswordCredentialsValidator : AbstractValidator<ForgotPasswordCredentialsApiModel>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ForgotPasswordCredentialsValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Password must not be empty");
               
        }
    }
}
