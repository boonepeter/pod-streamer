using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Podcaster.Converters
{
    /// <summary>
    /// Inverts boolean value
    /// </summary>
    public class DateTimeString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is null) return null;
            DateTime dt = DateTime.Parse(value.ToString());
            if (parameter is null)
            {
                parameter = "MM/dd/yyyy";
            }
            return dt.ToString((string)parameter);

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
