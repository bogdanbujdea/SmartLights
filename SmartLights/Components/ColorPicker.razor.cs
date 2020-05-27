using HomeAssistant;
using Microsoft.AspNetCore.Components;

namespace SmartLights.Components
{
    public partial class ColorPicker
    {
        [Parameter] public Light SelectedLight { get; set; }

        public async void ApplyColor()
        {
            StateHasChanged();
            await Assistant.ChangeColor(SelectedLight.Id, SelectedLight.Color);
        }
    }
}
