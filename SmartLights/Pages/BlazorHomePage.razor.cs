using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HomeAssistant;
using Microsoft.AspNetCore.Components;

namespace SmartLights.Pages
{
    partial class BlazorHomePage: ComponentBase
    {
        public Light SelectedLight { get; set; }

        public List<Light> Lights { get; set; } = new List<Light>();
        public bool IsColorPickerVisible { get; set; }

        public void ChangeColor(Light light)
        {
            IsColorPickerVisible = true;
            SelectedLight = light;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            Lights = await Assistant.GetLights();
        }
    }
}
