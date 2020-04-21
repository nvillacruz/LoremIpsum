using System;
using System.Globalization;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// A converter that takes in a boolean and inverts it
    /// </summary>
    public class ApplicationContentConverter : BaseValueConverter<ApplicationContentConverter>
    {
        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if(value is EnterpriseSettingsViewModel)
            {
                return new EnterpriseSettingView(value as EnterpriseSettingsViewModel);
            }

            return null;
        }

        /// <inheritdoc/>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
