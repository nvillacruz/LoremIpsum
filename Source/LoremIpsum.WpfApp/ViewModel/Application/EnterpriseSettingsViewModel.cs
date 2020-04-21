using Dna;
using LoremIpsum.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using Validar;
using static LoremIpsum.WpfApp.DI;
using static LoremIpsum.Core.CoreDI;
namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// View Model for the Enterprise Setting
    /// </summary>
    //[InjectValidationAttribute]
    public class EnterpriseSettingsViewModel : BaseValidationViewModel<EnterpriseSettingsViewModel>
    {
        /// <summary>
        /// Company Name
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Indicates if the enterprise setting is currently saving
        /// </summary>
        public bool Saving { get; set; }

        /// <summary>
        /// Indicates if the fetching of enterprise setting is loading
        /// </summary>
        public bool Loading { get; set; }

        /// <summary>
        /// Command that save setting
        /// </summary>
        public ICommand SaveSettingCommand { get; set; }

        /// <summary>
        /// Command that loads the enterprise settings
        /// </summary>
        public ICommand LoadCommand { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EnterpriseSettingsViewModel()
        {
            SaveSettingCommand = new RelayCommand(async () => await SaveSettingAsync());
            LoadCommand = new RelayCommand(async () => await LoadEnterPriseSettingsAsync());

            TaskManager.RunAndForget(LoadEnterPriseSettingsAsync);
        }

        public bool ErrorLoading { get; set; }

        public string ErrorText { get; set; }

        /// <summary>
        /// Save enterprise settings
        /// </summary>
        /// <returns></returns>
        public async Task SaveSettingAsync()
        {
            await RunCommandAsync(() => Saving, async () =>
            {
                if (HasErrors)
                    return;

                var credentials = await ClientDataStore.GetLoginCredentialsAsync();
                var result = await WebRequests.PostAsync<ApiResponse>(
                    url: RouteHelpers.GetAbsoluteRoute(ApiRoutes.UpdateEnterpriseSetting),
                    content: new UpdateEnterpriseSettingsApiModel
                    {
                        CompanyName = CompanyName
                    },
                    bearerToken: credentials.Token
                    );

                // If the response has an error...
                if (await result.HandleErrorIfFailedAsync("Update failed"))
                    // We are done
                    return;

            });
        }


        /// <summary>
        /// Loads the enterprise settings from database
        /// </summary>
        /// <returns></returns>
        public async Task LoadEnterPriseSettingsAsync()
        {
            // Lock this command to ignore any other requests while processing
            await RunCommandAsync(() => Loading, async () =>
            {
                ErrorLoading = false;
                ErrorText = default(string);
                // Get the current known credentials
                var credentials = await ClientDataStore.GetLoginCredentialsAsync();

                var result = await WebRequests.PostAsync<ApiResponse<EnterpriseSettingResultApiModel>>(
                    url: RouteHelpers.GetAbsoluteRoute(ApiRoutes.GetEnterpriseSetting),
                    bearerToken: credentials.Token
                );


                // If the response has an error...don't mind to continue
                if (await result.HandleErrorIfFailedAsync())
                {
                    ErrorLoading = true;
                    ErrorText = result.ErrorMessage;
                    return;
                }


                var dataModel = result.ServerResponse.Response;

                CompanyName = dataModel.CompanyName;
            });
        }




    }
}
