using LoremIpsum.Core;
using System.Diagnostics;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>
    public static class ApplicationPageHelpers
    {
        /// <summary>
        /// Takes a <see cref="ApplicationPage"/> and a view model, if any, and creates the desired page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static BaseUserControl ToBasePage(this ApplicationPage page, object viewModel = null)
        {
            // Find the appropriate page
            switch (page)
            {
                case ApplicationPage.Login:
                    return new LoginView(viewModel as LoginViewModel);

                case ApplicationPage.Register:
                    return new RegisterView(viewModel as RegisterViewModel);

                case ApplicationPage.Home:
                    return new HomeView(viewModel as HomeViewModel);

                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Converts a <see cref="BaseUserControl"/> to the specific <see cref="ApplicationPage"/> that is for that type of page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static ApplicationPage ToApplicationPage(this BaseUserControl page)
        {
            // Find application page that matches the base page
            if (page is LoginView)
                return ApplicationPage.Login;

            if (page is RegisterView)
                return ApplicationPage.Register;

            if (page is HomeView)
                return ApplicationPage.Home;

            // Alert developer of issue
            Debugger.Break();
            return default(ApplicationPage);
        }
    }
}
