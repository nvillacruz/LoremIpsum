using LoremIpsum.Core;
using System;
using System.Globalization;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// A converter that takes in a boolean and inverts it
    /// </summary>
    public class ApplicationModuleConverter : BaseValueConverter<ApplicationModuleConverter>
    {
        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Try and convert parameter string to enum
            if (!Enum.TryParse(parameter as string, out ApplicationModule type))
                return null;


            return type.ToTabItem();
        }

        /// <inheritdoc/>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    /// <summary>
    /// Extensions for <see cref="ApplicationModule"/>
    /// </summary>
    public static class ApplicationModuleExtensions
    {
        /// <summary>
        /// Converts <see cref="ApplicationModule"/> to <see cref="BaseTabItemViewModel"/>
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static BaseTabItemViewModel ToTabItem(this ApplicationModule module)
        {
            switch (module)
            {
                case ApplicationModule.EnterpriseSetting:
                    return new BaseTabItemViewModel<EnterpriseSettingsViewModel>
                    {
                        Label = "Enterprise Setting",
                    };

                default:
                    return null;
            }
        }
    }

}
