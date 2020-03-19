using System;
using System.Globalization;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml;

namespace Podcaster.Converters
{
    /// <summary>
    /// Convert from boolean to visibility collapsed if false
    /// </summary>
    public class BoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // obtain the conveter for the target type
            if (value is bool)
            {
                if ((bool)value == true) return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

    }
}
