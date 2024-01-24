using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NotePadClone.ValueConverters;

/// <summary>
/// Converts a <see cref="WindowState"/> to a <see cref="CornerRadius"/>.
/// </summary>
internal class WindowStateToCornerRadiusConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value is not WindowState state)
            throw new ArgumentException("The value must be of type WindowState.", nameof(value));

        double radius = -1;
        if (parameter is not null)
        {
            if (parameter is not string) 
                throw new ArgumentException("The parameter must be a string.", nameof(parameter));

            if (!double.TryParse(parameter.ToString(), out radius))
                throw new InvalidOperationException($"Could not parse parameter to double. Parameter: {parameter}");

            if (radius < 0)
                throw new InvalidOperationException($"Corner radius must be positive. Parameter: {parameter}");
        }

        if (radius < 0) radius = 10;

        return state == WindowState.Maximized ? new CornerRadius(0) : new CornerRadius(radius);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
