using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAssistant;
using SmartLights.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartLights.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XamlHomePage
    {
        private bool _isInitialized;
        private XamlLight _selectedLight;
        private List<XamlLight> _lights;

        public XamlHomePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var lights = await Assistant.GetLights();
            var xamlLights = lights.Select(l => new XamlLight
            {
                Color = l.Color,
                Id = l.Id,
                IsOn = l.IsOn,
                Name = l.Name
            }).ToList();
            Lights = xamlLights;
            await Task.Delay(1000);
            _isInitialized = true;
        }

        public List<XamlLight> Lights
        {
            get => _lights;
            set
            {
                _lights = value;
                OnPropertyChanged();
            }
        }

        public XamlLight SelectedLight
        {
            get => _selectedLight;
            set
            {
                _selectedLight = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LightIsSelected));
            }
        }

        public bool LightIsSelected => SelectedLight != null;

        private async void ToggleLight(object sender, EventArgs eventArgs)
        {
            if (_isInitialized == false)
                return;
            var light = (sender as Switch)?.BindingContext as XamlLight;

            await Assistant.ToggleLight(light?.Id);
        }

        private void ChangeColor(object sender, EventArgs e)
        {
            var light = (sender as Button)?.BindingContext as XamlLight;
            SelectedLight = light;
        }
    }
}