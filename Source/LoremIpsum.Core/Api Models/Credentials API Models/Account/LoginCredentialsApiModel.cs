namespace LoremIpsum.Core
{
    /// <summary>
    /// The credentials for  an API client to log into the server and receive a token back
    /// </summary>
    public class LoginCredentialsApiModel: ILoginCredentials
    {
        #region Public Properties

        /// <inheritdoc/>
        public string UsernameOrEmail { get; set; }

        /// <inheritdoc/>
        public string Password { get; set; }

        #endregion
    }
}
