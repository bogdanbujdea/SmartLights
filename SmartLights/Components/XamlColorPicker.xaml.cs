using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeAssistant;
using SmartLights.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartLights.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XamlColorPicker
    {
        public XamlColorPicker()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty SelectedLightProperty = BindableProperty.Create(
            propertyName: "SelectedLight",
            returnType: typeof(XamlLight),
            declaringType: typeof(XamlColorPicker),
            defaultValue: null);

        public XamlLight SelectedLight
        {
            get { return (XamlLight)GetValue(SelectedLightProperty); }
            set { SetValue(SelectedLightProperty, value); }
        }

        private async void ApplyColor(object sender, EventArgs e)
        {
            await Assistant.ChangeColor(SelectedLight.Id, SelectedLight.Color);
        }
    }
}