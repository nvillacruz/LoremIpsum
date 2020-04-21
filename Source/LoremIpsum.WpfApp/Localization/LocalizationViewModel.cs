using LoremIpsum.Core;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// The localization ViewModel of this application
    /// </summary>
    public class LocalizationViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// A model of the localization strings being loaded
        /// </summary>
        public CommonStringsModel Common { get; set; }

        /// <summary>
        /// Indicates if the loading of language is still running
        /// </summary>
        public bool IsLoading { get; set; }

        /// <summary>
        /// Current language of the application
        /// </summary>
        public LanguageType CurrentLanguage { get; private set; } = LanguageType.Unknown;

        #endregion

        #region Commands

        /// <summary>
        /// Command to change language
        /// </summary>
        public ICommand ChangeLanguageCommand { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public LocalizationViewModel()
        {
            ChangeLanguageCommand = new RelayParameterizedCommand(async param => await LoadAsync(param));
        }
        #endregion

        /// <summary>
        /// Loads the localization string based on the language passed
        /// </summary>
        /// <param name="param">The <see cref="LanguageType"/> passed in for the selected language</param>
        /// <returns></returns>
        public async Task LoadAsync(object param)
        {
            await RunCommandAsync(() => IsLoading, async () =>
           {

               var lang = (LanguageType)param;

               //If the parameter is the same with the current
               if (lang == CurrentLanguage)
                   //Ignore
                   return;

               //NOTE: Fetching the localized strings must be called from the API
               //For now, let's get it from this application

               //TODO: Get the localized strings from server
               var file = string.Empty;
               var filePath = "Localization\\{0}.json";

               switch (lang)
               {
                   case LanguageType.English:
                       file = await File.ReadAllTextAsync(string.Format(filePath, "en-US"));
                       break;
                   case LanguageType.France:
                       file = await File.ReadAllTextAsync(string.Format(filePath, "fr-FR"));
                       break;
                   case LanguageType.Chinese:
                       break;
                   default:
                       file = await File.ReadAllTextAsync(string.Format(filePath, "en-US"));
                       break;
               }

               //Set current language to the selected language
               CurrentLanguage = lang;

               //Deserialize file and pass it to common
               Common = JsonConvert.DeserializeObject<CommonStringsModel>(file);
           });
        }
    }
}
