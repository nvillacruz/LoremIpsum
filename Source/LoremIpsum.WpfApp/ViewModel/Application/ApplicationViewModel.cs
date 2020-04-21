using LoremIpsum.Core;
using System.Threading.Tasks;
using static LoremIpsum.WpfApp.DI;
using static LoremIpsum.Core.CoreDI;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
 
        #region Public Properties

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;

        /// <summary>
        /// The view model to use for the current page when the CurrentPage changes
        /// NOTE: This is not a live up-to-date view model of the current page
        ///       it is simply used to set the view model of the current page 
        ///       at the time it changes
        /// </summary>
        public BaseViewModel CurrentPageViewModel { get; set; }

        /// <summary>
        /// True if the side menu should be shown
        /// </summary>
        public bool SideMenuVisible { get; set; } = false;

        /// <summary>
        /// Determines if the application has network access to the main server
        /// </summary>
        public bool ServerReachable { get; set; } = false; 

        /// <summary>
        /// True if the login details should be shown
        /// </summary>
        public bool LoginDetailsVisible { get; set; }

        #endregion

        /// <summary>
        /// List of opened application tabs
        /// </summary>
        public BaseTabCollectionViewModel ApplicationTabs { get; set; }

        /// <summary>
        /// Commands that add an application tab
        /// </summary>
        public ICommand AddApplicationTabCommand { get; set; }


        #region Constructor

        /// <summary>
        /// The default constructor
        /// </summary>
        public ApplicationViewModel()
        {
            //var list = new List<BaseTabItemViewModel>
            //{
            //    new BaseTabItemViewModel<EnterpriseSettingsViewModel>
            //    {
            //        Label = "Enterprise Setting",
            //        ContentViewModel = new EnterpriseSettingsViewModel
            //        {
            //            CompanyName = "Company 1"
            //        }
            //    },

            //    new BaseTabItemViewModel<EnterpriseSettingsViewModel>
            //    {
            //        Label = "Setting1",
            //        ContentViewModel = new EnterpriseSettingsViewModel
            //        {
            //            CompanyName = "Company 2"
            //        }
            //    }
            //};

            ApplicationTabs = new BaseTabCollectionViewModel();

            AddApplicationTabCommand = new RelayParameterizedCommand(item => AddApplicationTab(item));

        }

        #endregion

        #region Public Helper Methods

        /// <summary>
        /// Navigates to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        /// <param name="viewModel">The view model, if any, to set explicitly to the new page</param>
        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {

            // Set the view model
            CurrentPageViewModel = viewModel;

            // See if page has changed
            var different = CurrentPage != page;

            // Set the current page
            CurrentPage = page;

            // If the page hasn't changed, fire off notification
            // So pages still update if just the view model has changed
            if (!different)
                OnPropertyChanged(nameof(CurrentPage));

            // Show side menu or not?
            SideMenuVisible = page == ApplicationPage.Home;

        }

        /// <summary>
        /// Handles what happens when we have successfully logged in
        /// </summary>
        /// <param name="loginResult">The results from the successful login</param>
        public async Task HandleSuccessfulLoginAsync(UserProfileDetailsApiModel loginResult)
        {
            // Store this in the client data store
            await ClientDataStore.SaveLoginCredentialsAsync(loginResult.ToLoginCredentialsDataModel());

            //Show Login Details
            LoginDetailsVisible = true;

            // Load new settings
            await ViewModelSettings.LoadAsync();



            // Go to home page
            ViewModelApplication.GoToPage(ApplicationPage.Home);
        }

        #endregion


        public void AddApplicationTab(object item)
        {
            // Try and convert parameter string to enum
            if (!Enum.TryParse(item as string, out ApplicationModule type))
                return ;

            ApplicationTabs.Add(type.ToTabItem());

        }
    }
}
