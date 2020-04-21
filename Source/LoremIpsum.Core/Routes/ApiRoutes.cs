namespace LoremIpsum.Core
{
    /// <summary>
    /// The relative routes to all Api calls in the server
    /// </summary>
    public static class ApiRoutes
    {
        /// <summary>
        /// Api Route for login
        /// </summary>
        public const string Login = "api/login";
        /// <summary>
        /// API Route for logout
        /// </summary>
        public const string LogOut = "api/logout";
        /// <summary>
        /// 
        /// </summary>
        public const string Register = "api/register";
        /// <summary>
        /// 
        /// </summary>
        public const string GetCurrentUser = "api/getcurrentuser";
        /// <summary>
        /// 
        /// </summary>
        public const string UpdateUser = "api/updateuser";
        /// <summary>
        /// 
        /// </summary>
        public const string GetUserProfile = "api/user/profile";
        /// <summary>
        /// 
        /// </summary>
        public const string UpdateAccountSettings = "api/updateaccountsettings";
        /// <summary>
        /// 
        /// </summary>
        public const string FaceBookLogin = "api/externalauth/facebook";
        /// <summary>
        /// 
        /// </summary>
        public const string VerifyEmail = "api/verify/email/{userId}/{*emailToken}";
        /// <summary>
        /// 
        /// </summary>
        public const string SendForgotPasswordLink = "api/forgot/password";
        /// <summary>
        /// 
        /// </summary>
        public const string ResetPassword = "api/reset/password/";

        /// <summary>
        /// 
        /// </summary>
        public const string Seed = "api/seed/";

        /// <summary>
        /// 
        /// </summary>
        public const string GetLocalizedStrings = "api/lang/{lang}";

        /// <summary>
        /// 
        /// </summary>
        public const string GetEnterpriseSetting= "api/getEnterpriseSetting";

        /// <summary>
        /// 
        /// </summary>
        public const string UpdateEnterpriseSetting = "api/updateEnterpriseSetting";


    }
}
