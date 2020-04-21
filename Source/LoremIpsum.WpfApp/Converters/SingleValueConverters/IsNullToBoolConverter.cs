using System;
using System.Globalization;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// A converter that takes an object and returns true if it is null
    /// </summary>
    public class IsNullToBoolConverter : BaseValueConverter<IsNullToBoolConverter>
    {
        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value == null;

        /// <inheritdoc/>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
