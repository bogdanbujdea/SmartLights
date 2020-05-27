using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using HomeAssistant;
using Xamarin.Forms;

namespace SmartLights.Converters
{
    public class ColorToXFColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LightColor color)
                return color.ToXFColor();
            return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
