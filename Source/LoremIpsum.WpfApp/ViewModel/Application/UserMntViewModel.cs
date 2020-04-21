using Dna;
using LoremIpsum.Core;
using System.Threading.Tasks;
using static LoremIpsum.Core.CoreDI;
using static LoremIpsum.WpfApp.DI;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// ViewModel for User Maintenance
    /// </summary>
    public class UserMntViewModel : BaseViewModel
    {
        /// <summary>
        /// List of users
        /// </summary>
        public ObservableCollectionWithSelection<UserItemViewModel> Users { get; set; }

        /// <summary>
        /// Indicates if the list of users is currently being loaded
        /// </summary>
        public bool UsersLoading { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserMntViewModel()
        {
            TaskManager.RunAndForget(GetUsersAsync);
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        public async Task GetUsersAsync()
        {

            await RunCommandAsync(() => UsersLoading, async () =>
            {
                await Task.Delay(1);

                var credentials = await ClientDataStore.GetLoginCredentialsAsync();

                var result = await WebRequests.PostAsync<ApiResponse<UserResultApiModel>>(
                    url: RouteHelpers.GetAbsoluteRoute(ApiRoutes.GetEnterpriseSetting),
                    bearerToken: credentials.Token
                );


                // If the response has an error...don't mind to continue
                if (await result.HandleErrorIfFailedAsync())
                {
                    return;
                }
            });
        }
    }



    public class UserItemViewModel : BaseViewModel
    {

    }

    /// <summary>
    /// ViewModel for single view of User Maintenance
    /// </summary>
    public class UserMntSingleViewModel : BaseValidationViewModel<UserMntViewModel>
    {

    }


}
