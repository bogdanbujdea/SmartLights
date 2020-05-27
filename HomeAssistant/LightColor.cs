namespace HomeAssistant
{
    public class LightColor
    {
        public LightColor()
        {
        }

        public LightColor(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public double Alpha { get; set; } = 255;

        public double Red { get; set; }

        public double Green { get; set; }

        public double Blue { get; set; }

        public static LightColor Default { get; set; } = new LightColor{ Alpha = 0 };
    }
}