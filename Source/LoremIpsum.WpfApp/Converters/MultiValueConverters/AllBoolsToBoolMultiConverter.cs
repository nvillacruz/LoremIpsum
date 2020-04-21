using System;
using System.Globalization;
using System.Linq;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// Takes multiple bools as input and returns true if all bools are true.
    /// </summary>
    public class AllBoolsToBoolMultiConverter : BaseMultiValueConverter<AllBoolsToBoolMultiConverter>
    {
        /// <inheritdoc/>
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.OfType<bool>().All(x => x);
        }

        /// <inheritdoc/>
        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
