using Microsoft.AspNetCore.Identity;

namespace LoremIpsum.Domain
{
    /// <summary>
    /// The user data and profile for our application
    /// </summary>
    /// 
    public class ApplicationUser : IdentityUser
    {
        #region Navigational Properties

        /// <summary>
        /// Navigational Property for <see cref="Employee"/>
        /// </summary>
        public  Employee Employee { get; set; }
        
        #endregion

    }
}