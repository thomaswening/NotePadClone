using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NotePadClone.ValueConverters;

/// <summary>
/// Converts a number to a string, given an optional format string and culture.
/// </summary>
public class IntToStringConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is null)
            return null;

        if (value is not int number) throw new ArgumentException("Value must be a number.");

        if (parameter is not string formatString)
        {
            return number.ToString(culture);
        }

        return number.ToString(formatString, culture);
    }

    public object? ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is null) return null;

        if (value is not string numberString) throw new ArgumentException("Value must be a string.");

        if (int.TryParse(numberString, out int number))
        {
            return number;
        }

        throw new ArgumentException("Value must be a number.", nameof(value));
    }
}