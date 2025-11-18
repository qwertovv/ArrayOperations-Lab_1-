using System;
using System.Globalization;
using System.Windows.Data;

namespace ArrayOperations
{
    public class PreconditionTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolValue
                ? $"Предусловие: {(boolValue ? "выполнено" : "не выполнено")}"
                : "Предусловие: неизвестно";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}