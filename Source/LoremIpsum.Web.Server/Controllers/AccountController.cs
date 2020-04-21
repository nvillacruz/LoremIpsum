using Dna;
using LoremIpsum.Core;
using LoremIpsum.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static LoremIpsum.Core.GuidHelper;
using static LoremIpsum.Core.CoreDI;
using System.Security.Claims;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// Manages the Web API calls for Accounts
    /// </summary>
    [Produces("application/json")]
    [AuthorizeToken]
    public class AccountController : BaseController
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">The injected context</param>
        /// <param name="signInManager">The Identity sign in manager</param>
        /// <param name="userManager">The Identity user manager</param>
        /// /// <param name="roleManager">The Identity user manager</param>
        public AccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager
            ) : base(context, userManager, signInManager, roleManager)
        {

        }

        #endregion

        /// <summary>
        /// Tries to register for a new account on the server
        /// </summary>
        /// <param name="registerCredentials">The registration details</param>
        /// <returns>Returns the result of the register request</returns>
        [Route(ApiRoutes.Register)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse<RegisterResultApiModel>> RegisterAsync([FromBody]RegisterCredentialsApiModel registerCredentials)
        {
            var errorResponse = new ApiResponse<RegisterResultApiModel>
            {
                ErrorMessage = UserMessages.UserNotFound
            };
            //TODO: Allow registration of username only

            var userId = GenerateNewGuid();
            // Create the desired user from the given details
            var user = new ApplicationUser
            {
                UserName = registerCredentials.Username,
                Email = registerCredentials.Email,
                PhoneNumber = registerCredentials.PhoneNumber,
                Employee = new Employee
                {
                    Id = userId,
                    Gender = (Gender)registerCredentials.Gender,
                    FirstName = registerCredentials.FirstName,
                    LastName = registerCredentials.LastName,
                    CreatedBy = userId,
                    ModifiedBy = userId,
                }
            };

            //Check if email is unique
            var isNotUnique = await mContext.Users.Where(x => x.NormalizedEmail == registerCredentials.Email.NormalizedUpper()).Select(x => true).SingleOrDefaultAsync();

            if (isNotUnique)
            {
                errorResponse.ErrorMessage = UserMessages.EmailHasBeenTakenAlready;
                return errorResponse;
            }


            using var transaction = await mContext.Database.BeginTransactionAsync();

            try
            {
                // Try and create a user
                var result = await mUserManager.CreateAsync(user, registerCredentials.Password);

                // If the registration was successful...
                if (result.Succeeded)
                {
                    // Get the user details
                    var userIdentity = await mUserManager.Users.Include(x => x.Employee).Select(x => x).FirstOrDefaultAsync(x => x.UserName == registerCredentials.Username);

                    //Add Employee Role to the user
                    await mUserManager.AddToRoleAsync(userIdentity, AspNetRolesDefaults.Employee);


                    //Send email verification
                    //await SendUserEmailVerificationAsync(userIdentity);


                    //Commit the whole transaction
                    await transaction.CommitAsync();

                    // Return valid response containing necessary users details
                    return new ApiResponse<RegisterResultApiModel>
                    {
                        Response = new RegisterResultApiModel
                        {
                            FirstName = userIdentity.Employee.FirstName,
                            LastName = userIdentity.Employee.LastName,
                            Token = userIdentity.GenerateJwtToken(),
                        }
                    };


                }
                // Otherwise if it failed...
                else
                {
                    throw new Exception(result.Errors.AggregateErrors());
                }
            }
            catch (Exception ex)
            {
                //Rollback the whole transaction
                await transaction.RollbackAsync();

                // Return the failed response
                return new ApiResponse<RegisterResultApiModel>
                {
                    ErrorMessage = ex.Message
                };
            };
        }


        /// <summary>
        /// Logs in a user using token-based authentication
        /// </summary>
        /// <returns>Returns the result of the login request</returns>
        [Route(ApiRoutes.Login)]
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<ApiResponse<UserProfileDetailsApiModel>> LogInAsync([FromBody]LoginCredentialsApiModel loginCredentials)
        {
            //Create default error message
            var errorResponse = new ApiResponse<UserProfileDetailsApiModel>
            {
                ErrorMessage = UserMessages.InvalidUserNameOrPassword
            };

            return await TaskManager.Run(async () =>
            {
                // Validate if the user credentials are correct...
                // Is it an email?
                var isEmail = loginCredentials.UsernameOrEmail.Contains("@");

                // Get the user details
                var user = isEmail ?
                     // Find by email
                     await mUserManager.Users
                     .Include(x => x.Employee)
                     .FirstOrDefaultAsync(y => y.Email == loginCredentials.UsernameOrEmail) :
                     // Find by username
                     await mUserManager.Users
                     .Include(x => x.Employee)
                     .FirstOrDefaultAsync(y => y.UserName == loginCredentials.UsernameOrEmail);

                // If we failed to find a user...
                if (user == null)
                    // Return error message to user
                    return errorResponse;

                // If we got here we have a user...
                // Let's validate the password

                // Check if password is valid
                var isValidPassword = await mUserManager.CheckPasswordAsync(user, loginCredentials.Password);

                // If the password was wrong
                if (!isValidPassword)
                    // Return error message to user
                    return errorResponse;

                // Get if the user is confirm
                var isConfirmed = await mUserManager.IsEmailConfirmedAsync(user);
                // if not confirm
                if (!isConfirmed)
                {
                    errorResponse.ErrorMessage = UserMessages.EmailNotConfirmed;
                    // Return error message to user
                    return errorResponse;
                }

                // If we get here, we are valid and the user passed the correct login details

                // Get username
                var username = user.UserName;

                ////Sign user in with the valid credentials
                //var res = await mSignInManager.PasswordSignInAsync(user, loginCredentials.Password, true, false);

                var a = user.Employee;
                // Return token to user
                return new ApiResponse<UserProfileDetailsApiModel>
                {
                    // Pass back the user details and the token
                    Response = new UserProfileDetailsApiModel
                    {
                        FirstName = user.Employee.FirstName,
                        LastName = user.Employee.LastName,
                        Username = user.UserName,
                        Email = user.Email,
                        Token = user.GenerateJwtToken(),
                    }
                };


            });
        }
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="emailToken"></param>
        /// <returns></returns>
        [Route(ApiRoutes.VerifyEmail)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse<VerifyEmailResultApiModel>> VerifyEmailAsync(string userId, string emailToken)
        {
            // https://stackoverflow.com/questions/37178949/how-do-i-allow-url-encoded-path-segments-in-azure


            var response = new ApiResponse<VerifyEmailResultApiModel>
            {
                Response = new VerifyEmailResultApiModel
                {

                }
            };

            // Get the user
            var user = await mUserManager.FindByIdAsync(userId);

            // NOTE: Issue at the minute with Url Decoding that contains /'s does not replace them
            //       https://github.com/aspnet/Home/issues/2669
            //       
            //       For now, manually fix that
            emailToken = emailToken.Replace("%2f", "/").Replace("%2F", "/");

            // If the user is null
            if (user == null)
                response.ErrorMessage = UserMessages.UserNotFound;

            // Verify the email token
            var result = await mUserManager.ConfirmEmailAsync(user, emailToken);

            // If succeeded...
            if (result.Succeeded)
            {
                response.Response = new VerifyEmailResultApiModel
                {
                    Message = UserMessages.AccountActivated
                };

                return response;
            }

            response.ErrorMessage = UserMessages.InvalidEmailToken;
            // TODO: Provide nice UI
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [Route(ApiRoutes.SendForgotPasswordLink)]
        [HttpPost]
        [AllowAnonymous]
        [ValidateModel]
        public async Task<ApiResponse<ForgotPasswordResultApiModel>> SendForgotPasswordLinkAsync([FromBody]ForgotPasswordCredentialsApiModel credentials)
        {
            var response = new ApiResponse<ForgotPasswordResultApiModel>
            {
                ErrorMessage = UserMessages.EmailNotRegistered
            };

            var user = await mUserManager.Users.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Email == credentials.Email);
            if (user == null)
            {
                response.ErrorMessage = UserMessages.EmailNotRegistered;
                return response;
            }

            var code = await mUserManager.GeneratePasswordResetTokenAsync(user);
            var confirmationUrl = $"{Framework.Construction.Configuration["Client:Host"]}/reset-password/{HttpUtility.UrlEncode(user.Id)}/{HttpUtility.UrlEncode(code)}";

            var b = await LoremIpsumEmailSender.SendUserForgotPasswordLinkAsync(user.Employee.FirstName, user.Email, confirmationUrl);

            if (b.Successful)
            {
                return new ApiResponse<ForgotPasswordResultApiModel>
                {
                    Response = new ForgotPasswordResultApiModel
                    {
                        Message = UserMessages.PasswordResetMessageSent,
                        CallBackUrl = confirmationUrl
                    }
                };
            }
            else
            {
                return new ApiResponse<ForgotPasswordResultApiModel>
                {
                    ErrorMessage = b.Errors.ToString(),
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        [Route(ApiRoutes.ResetPassword)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponse<ResetPasswordResultApiModel>> ResetPasswordAsync([FromBody]ResetPasswordCredentialsApiModel credentials)
        {

            var errorResponse = new ApiResponse<ResetPasswordResultApiModel>
            {
                ErrorMessage = UserMessages.UserNotFound
            };

            // Get the user
            var user = await mUserManager.FindByIdAsync(credentials.UserId);

            // NOTE: Issue at the minute with Url Decoding that contains /'s does not replace them
            //       https://github.com/aspnet/Home/issues/2669
            //       
            //       For now, manually fix that
            var code = credentials.Code.Replace("%2f", "/").Replace("%2F", "/");

            if (user == null)
                return errorResponse;


            // Verify the reset code
            var result = await mUserManager.ResetPasswordAsync(user, code, credentials.NewPassword);

            // If succeeded...
            if (result.Succeeded)
                return new ApiResponse<ResetPasswordResultApiModel>
                {
                    Response = new ResetPasswordResultApiModel
                    {
                        Email = user.Email,
                        Message = UserMessages.PasswordHasBeenReset

                    }
                };

            errorResponse.ErrorMessage = result.Errors?.ToList()
                        .Select(f => f.Description)
                        .Aggregate((a, b) => $"{a}{Environment.NewLine}{b}");
            return errorResponse;

        }


        /// <summary>
        /// Logs out the user
        /// </summary>
        /// <returns></returns>
        [Route(ApiRoutes.LogOut)]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResponse<string>> LogOutAsync()
        {
            try
            {

                await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

                return new ApiResponse<string>
                {
                    Response = ""
                    
                };
            }
            catch (Exception e)
            {
                return new ApiResponse<string>
                {
                    ErrorMessage = e.InnerException.Message
                };
            }
        }


        [Route(ApiRoutes.GetUserProfile)]
        public async Task<ApiResponse<UserProfileDetailsApiModel>> GetUserProfileAsync()
        {

            var response = new ApiResponse<UserProfileDetailsApiModel>
            {
                ErrorMessage = UserMessages.UserNotFound
            };

            var currentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserID))
                return response;

            var user = await mUserManager.Users
                    .Include(x => x.Employee)
                    .FirstOrDefaultAsync(x => x.Id == currentUserID);

            if (user == null)
                return response;

            return new ApiResponse<UserProfileDetailsApiModel>
            {
                // Pass back the user details
                Response = new UserProfileDetailsApiModel
                {
                    FirstName = user.Employee.FirstName,
                    LastName = user.Employee.LastName,
                    Email = user.Email,
                    Username = user.UserName,
                    Token = user.GenerateJwtToken()
                }
            };
        }

        #region Private Methods 

        #endregion

        #region Private Helpers

        /// <summary>
        /// Sends the given user a new verify email link
        /// </summary>
        /// <param name="user">The user to send the link to</param>
        /// <returns></returns>
        private async Task SendUserEmailVerificationAsync(ApplicationUser user)
        {

            // Generate an email verification code
            var emailVerificationCode = await mUserManager.GenerateEmailConfirmationTokenAsync(user);

            //Set the URL of the Api client that will verify the user
            var confirmationUrl = $"{Framework.Construction.Configuration["Client:Host"]}/email-verified/{HttpUtility.UrlEncode(user.Id)}/{HttpUtility.UrlEncode(emailVerificationCode)}";

            // Email the user the verification code
            await LoremIpsumEmailSender.SendUserVerificationEmailAsync(user.Employee.FirstName, user.Email, confirmationUrl);
        }

        #endregion
    }
}
