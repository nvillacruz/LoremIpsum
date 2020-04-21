using Dna;
using FluentValidation;
using LoremIpsum.Core;
using Microsoft.Extensions.DependencyInjection;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// Extension methods for the <see cref="FrameworkConstruction"/>
    /// </summary>
    public static class FrameworkConstructionExtensions
    {
        /// <summary>
        /// Injects the view models needed for Lorem Ipsum application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddLoremIpsumViewModels(this FrameworkConstruction construction)
        {
            // Bind to a single instance of Application view model
            construction.Services.AddSingleton<ApplicationViewModel>();

            // Bind to a single instance of Settings view model
            construction.Services.AddSingleton<SettingsViewModel>();

            // Bind to a single instance of Localization view model
            construction.Services.AddSingleton<LocalizationViewModel>();

            // Return the construction for chaining
            return construction;
        }

        /// <summary>
        /// Injects the Lorem Ipsum client application services needed
        /// for the application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddLoremIpsumClientServices(this FrameworkConstruction construction)
        {

            // Add our task manager
            construction.Services.AddTransient<ITaskManager, BaseTaskManager>();

            // Bind a file manager
            construction.Services.AddTransient<IFileManager, BaseFileManager>();

            // Bind a UI Manager
            construction.Services.AddTransient<IUIManager, UIManager>();

            // Return the construction for chaining
            return construction;
        }

        /// <summary>
        /// Add fluent validators for some ViewModels
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddFluentValidators(this FrameworkConstruction construction)
        {

            construction.Services.AddTransient<IValidator<EnterpriseSettingsViewModel>, EnterpriseSettingsViewModelValidator>();

            return construction;
        }

    }
}
