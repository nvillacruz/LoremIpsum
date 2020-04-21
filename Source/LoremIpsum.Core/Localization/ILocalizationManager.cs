using System.Threading.Tasks;

namespace LoremIpsum.Core
{
    /// <summary>
    /// Interface that handles the Localization of the application
    /// </summary>
    public interface ILocalizationManager
    {
        /// <summary>
        /// Gets the localized strings based on the <see cref="LanguageType"/> passed
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        Task<CommonStringsModel> GetLocalizedStrings(LanguageType language);
    }
}
