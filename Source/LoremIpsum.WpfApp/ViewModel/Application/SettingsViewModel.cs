using Dna;
using LoremIpsum.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using static LoremIpsum.WpfApp.DI;
using static Dna.FrameworkDI;
using System.Linq.Expressions;
using System.Diagnostics;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// The settings state as a view model
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Indicates if the settings details are currently being loaded
        /// </summary>
        public bool SettingsLoading { get; set; }

        /// <summary>
        /// Flag to show the login details popup
        /// </summary>
        public bool ShowLoginDetails { get; set; }


        /// <summary>
        /// The command to logout of the application
        /// </summary>
        public ICommand LogoutCommand { get; set; }

        /// <summary>
        /// The command to show the login details as popup
        /// </summary>
        public ICommand ShowLoginDetailsCommand { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsViewModel()
        {
            LogoutCommand = new RelayCommand(async () => await LogoutAsync());
            ShowLoginDetailsCommand = new RelayCommand(async () => await ShowLoginDetailsAsync());
        }


        /// <summary>
        /// Sets the settings view model properties based on the data in the client data store
        /// </summary>
        public async Task LoadAsync()
        {
            // Lock this command to ignore any other requests while processing
            await RunCommandAsync(() => SettingsLoading, async () =>
            {
                // Store single transcient instance of client data store
                var scopedClientDataStore = ClientDataStore;

                // Update values from local cache
                await UpdateValuesFromLocalStoreAsync(scopedClientDataStore);

                // Get the user token
                var token = (await scopedClientDataStore.GetLoginCredentialsAsync())?.Token;

                // If we don't have a token (so we are not logged in...)
                if (string.IsNullOrEmpty(token))
                    // Then do nothing more
                    return;
                else
                    ViewModelApplication.LoginDetailsVisible = true;


                return;
                // Load user profile details form server
                var result = await WebRequests.PostAsync<ApiResponse<UserProfileDetailsApiModel>>(
                    // Set URL
                    RouteHelpers.GetAbsoluteRoute(ApiRoutes.GetUserProfile),
                    // Pass in user Token
                    bearerToken: token);

                // If the response has an error...
                if (await result.HandleErrorIfFailedAsync("Load User Details Failed"))
                    // We are done
                    return;

                // TODO: Should we check if the values are different before saving?

                // Create data model from the response
                var dataModel = result.ServerResponse.Response.ToLoginCredentialsDataModel();

                // Re-add our known token
                dataModel.Token = token;

                // Save the new information in the data store
                await scopedClientDataStore.SaveLoginCredentialsAsync(dataModel);

                // Update values from local cache
                await UpdateValuesFromLocalStoreAsync(scopedClientDataStore);
            });
        }


        /// <summary>
        /// Loads the settings from the local data store and binds them 
        /// to this view model
        /// </summary>
        /// <returns></returns>
        private async Task UpdateValuesFromLocalStoreAsync(IClientDataStore clientDataStore)
        {
            // Get the stored credentials
            var storedCredentials = await clientDataStore.GetLoginCredentialsAsync();

            // Set first name
            FirstName = storedCredentials?.FirstName;

            // Set last name
            LastName = storedCredentials?.LastName;

            // Set username
            Username = storedCredentials?.Username;

            // Set email
            Email = storedCredentials?.Email;
        }


        /// <summary>
        /// Indicates if the user is currently logging out
        /// </summary>
        public bool LoggingOut { get; set; }

        /// <summary>
        /// Logs the user out
        /// </summary>
        public async Task LogoutAsync()
        {
            // Lock this command to ignore any other requests while processing
            await RunCommandAsync(() => LoggingOut, async () =>
            {
                //Hide the popup
                ShowLoginDetails = false;

                // TODO: Confirm the user wants to logout

                // Clear any user data/cache
                await ClientDataStore.ClearAllLoginCredentialsAsync();

                //Remove all opened application tabs
                ViewModelApplication.ApplicationTabs.Clear();

                // Clean all application level view models that contain
                // any information about the current user

                ClearUserData();

                ViewModelApplication.LoginDetailsVisible = false;

                // Go to login page
                ViewModelApplication.GoToPage(ApplicationPage.Login);
            });
        }

        /// <summary>
        /// Clears any data specific to the current user
        /// </summary>
        public void ClearUserData()
        {
            // Clear the users info
            FirstName = null;
            LastName = null;
            Username = null;
            Email = null;
        }

        /// <summary>
        /// Shows the login details popup
        /// </summary>
        /// <returns></returns>
        public async Task ShowLoginDetailsAsync()
        {
            //await RunCommandAsync(
            //    () => ShowLoginDetails,
            //    async () =>
            //    {
                    await Task.Run(() => ShowLoginDetails = !ShowLoginDetails);
            //    }
            //);
        }
    }

}
