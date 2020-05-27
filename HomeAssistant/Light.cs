using HADotNet.Core;
using HADotNet.Core.Clients;
using Newtonsoft.Json;

namespace HomeAssistant
{
    public class Light
    {
        private bool _isOn;
        public string Id { get; set; }

        public string Name { get; set; }

        public void SetInitialState(bool isOn, LightColor color)
        {
            IsOn = isOn;
            Color = color;
        }

        public bool IsOn
        {
            get => _isOn;
            set
            {
                _isOn = value;
                Toggle();
            }
        }

        public LightColor Color { get; set; }

        public async void Toggle()
        {
            await Assistant.ToggleLight(Id);
        }
    }
}