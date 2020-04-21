using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// The authorization policy for token-based authentication
    /// </summary>
    public class AuthorizeTokenAttribute : AuthorizeAttribute
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AuthorizeTokenAttribute()
        {
            // Add the JWT bearer authentication scheme
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }

        #endregion
    }


    public class ValidateHashAuthenticationSchemeOptions
       : AuthenticationSchemeOptions
    { }



    public class ValidateHashAuthenticationHandler
        : AuthenticationHandler<ValidateHashAuthenticationSchemeOptions>
    {
        public ValidateHashAuthenticationHandler(
            IOptionsMonitor<ValidateHashAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new System.NotImplementedException();
        }
    }
  
}
