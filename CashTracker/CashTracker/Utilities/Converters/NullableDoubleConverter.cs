using System;
using System.Globalization;
using Xamarin.Forms;

namespace CashTracker.Utilities.Converters
{
    public class NullableDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nullable = value as double?;
            return nullable?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;
            double? result = null;

            if (double.TryParse(stringValue, out var doubleValue))
            {
                result = new Nullable<double>(doubleValue);
            }

            return result;
        }
    }
}
