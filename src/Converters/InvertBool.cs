using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Podcaster.Converters
{
    /// <summary>
    /// Inverts boolean value
    /// </summary>
    public class InvertBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(value is true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(bool)value;
        }
    }
}
