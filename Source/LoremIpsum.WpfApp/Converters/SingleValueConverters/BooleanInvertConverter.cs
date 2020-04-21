using System;
using System.Globalization;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// A converter that takes in a boolean and inverts it
    /// </summary>
    public class BooleanInvertConverter : BaseValueConverter<BooleanInvertConverter>
    {
        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;

        /// <inheritdoc/>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
