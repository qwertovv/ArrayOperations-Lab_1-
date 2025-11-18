using System;
using System.Globalization;
using System.Windows.Data;

namespace ArrayOperations
{
    public class PostconditionTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolValue
                ? $"Постусловие: {(boolValue ? "выполнено" : "не выполнено")}"
                : "Постусловие: неизвестно";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}