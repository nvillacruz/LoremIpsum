using System;
using System.Globalization;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// Returns true if an object is immutable
    /// </summary>
    public class IsImmutableToBoolConverter : BaseValueConverter<IsImmutableToBoolConverter>
    {
        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return true;

            var type = value.GetType();

            return type.IsValueType
                || type.IsEnum
                || value is string
                || value is Delegate;
        }

        /// <inheritdoc/>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
