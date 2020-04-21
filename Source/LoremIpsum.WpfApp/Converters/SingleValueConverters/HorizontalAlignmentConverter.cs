using LoremIpsum.Core;
using System;
using System.Globalization;
using System.Windows;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// A converter that takes in the core horizontal alignment enum and converts it to a WPF alignment
    /// </summary>
    public class HorizontalAlignmentConverter : BaseValueConverter<HorizontalAlignmentConverter>
    {
        /// <inheritdoc/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (HorizontalAlignment)value;
        }
        /// <inheritdoc/>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
