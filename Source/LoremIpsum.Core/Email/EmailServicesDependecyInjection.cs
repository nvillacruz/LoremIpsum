using Dna;
using Microsoft.Extensions.DependencyInjection;

namespace LoremIpsum.Core
{
    /// <summary>
    /// A shorthand access class to get DI services with nice clean short code
    /// </summary>
    public static class EmailServicesDependecyInjection
    {
      
        /// <summary>
        /// The transient instance of the <see cref="IEmailSender"/>
        /// </summary>
        public static IEmailSender EmailSender => Framework.Provider.GetService<IEmailSender>();


        /// <summary>
        /// The transient instance of the <see cref="IEmailTemplateSender"/>
        /// </summary>
        public static IEmailTemplateSender EmailTemplateSender => Framework.Provider.GetService<IEmailTemplateSender>();
    }

    
}
