using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Podcaster.Converters
{
    /// <summary>
    /// Inverts boolean value
    /// </summary>
    public class NotNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
