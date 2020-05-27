using HomeAssistant;
using Xamarin.Forms;

namespace SmartLights.Converters
{
    public static class ConverterExtensions
    {
        public static Color ToXFColor(this LightColor lightColor)
        {
            return Color.FromRgba(lightColor.Red, lightColor.Green, lightColor.Blue, lightColor.Alpha);
        }
    }
}
