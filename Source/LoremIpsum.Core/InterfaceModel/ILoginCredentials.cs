namespace LoremIpsum.Core
{
    /// <summary>
    /// Interface for login credentials
    /// </summary>
    public interface ILoginCredentials
    {
        /// <summary>
        /// The users username or email
        /// </summary>
         string UsernameOrEmail { get; set; }

        /// <summary>
        /// The users password
        /// </summary>
         string Password { get; set; }

    }
}
