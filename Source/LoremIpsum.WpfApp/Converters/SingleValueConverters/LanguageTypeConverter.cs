using LoremIpsum.Core;
using System;
using System.Globalization;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// A converter that takes a language string and converts it to <see cref="LanguageType"/>
    /// </summary>
    public class LanguageTypeConverter : BaseValueConverter<LanguageTypeConverter>
    {
        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If we have no parameter return invisible
            if (parameter == null)
                return LanguageType.English;

            // Try and convert parameter string to enum
            if (!Enum.TryParse(parameter as string, out LanguageType type))
                return LanguageType.English;

            return type;
        }

        /// <inheritdoc/>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
