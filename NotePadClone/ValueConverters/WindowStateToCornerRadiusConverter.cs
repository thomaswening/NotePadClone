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
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is not WindowState state)
            throw new ArgumentException("The value must be of type WindowState", nameof(value));

        if (parameter is not null and double)
            throw new ArgumentException("The parameter must be of type double", nameof(parameter));

        double radius = parameter is double d ? d : 25;            

        return state == WindowState.Maximized ? new CornerRadius(0) : new CornerRadius(radius);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
