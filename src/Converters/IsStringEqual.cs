using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Podcaster.Converters
{
    /// <summary>
    /// Inverts boolean value
    /// </summary>
    public class IsStringEqual : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (string)value == (string)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
