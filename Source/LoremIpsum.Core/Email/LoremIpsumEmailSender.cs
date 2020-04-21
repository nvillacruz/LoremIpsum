using Dna;
using System;
using System.Threading.Tasks;

namespace LoremIpsum.Core
{
    /// <summary>
    /// Handles sending emails specific to the Yhotel
    /// </summary>
    public static class LoremIpsumEmailSender
    {
        /// <summary>
        /// Sends a verification email to the specified user
        /// </summary>
        /// <param name="displayName">The users display name (typically first name)</param>
        /// <param name="email">The users email to be verified</param>
        /// <param name="verificationUrl">The URL the user needs to click to verify their email</param>
        /// <returns></returns>
        public static async Task<SendEmailResponse> SendUserVerificationEmailAsync(string displayName, string email, string verificationUrl)
        {
            return await EmailServicesDependecyInjection.EmailTemplateSender.SendGeneralEmailAsync(new SendEmailDetails
            {
                IsHTML = true,
                FromEmail = Framework.Construction.Configuration["EmailSettings:SendEmailFromEmail"],
                FromName = Framework.Construction.Configuration["EmailSettings:SendEmailFromName"],
                ToEmail = email,
                ToName = displayName,
                Subject = "Y Bizz Account - Email Verification"
            },
            "Account Verification",
            $"Hi {displayName ?? "stranger"},",
            "Thank you for creating an account with us.<br/> " +
            "You are only one step away before you can avail all our services. <br/>" +
            "Click the button below to confirm your email",
            "Verify Email",
            verificationUrl
            );
        }

        /// <summary>
        /// Sends a forgot password link to the specified user
        /// </summary>
        /// <param name="displayName">The users display name (typically first name)</param>
        /// <param name="email">The users email to be verified</param>
        /// <param name="verificationUrl">The URL the user needs to click to go for the page of Resetting Password</param>
        /// <returns></returns>
        public static async Task<SendEmailResponse> SendUserForgotPasswordLinkAsync(string displayName, string email, string verificationUrl)
        {
            var a = await EmailServicesDependecyInjection.EmailTemplateSender.SendGeneralEmailAsync(new SendEmailDetails
            {
                IsHTML = true,
                FromEmail = Framework.Construction.Configuration["EmailSettings:SendEmailFromEmail"],
                FromName = Framework.Construction.Configuration["EmailSettings:SendEmailFromName"],
                ToEmail = email,
                ToName = displayName,
                Subject = "Forgot Password Link"
            },
                "Reset Password",
                $"Hi {displayName ?? "stranger"},",
                "It looks like you are trying to reset your password",
                "Go to Reset Password Page",
                verificationUrl
            );

            return a;
        }
   
    }
}
