using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoremIpsum.Core
{
    /// <summary>
    /// A class that handles the localization of the application
    /// </summary>
    public class LocalizationManager : ILocalizationManager
    {
        ///<inheritdoc/>
        public Task<CommonStringsModel> GetLocalizedStrings(LanguageType language)
        {
            //TODO: Implement the fetching of localized strings
            throw new NotImplementedException();
        }
    }
}
