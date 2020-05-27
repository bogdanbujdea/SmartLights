using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using HomeAssistant;
using SmartLights.Annotations;

namespace SmartLights.Models
{
    public class XamlLight: INotifyPropertyChanged
    {
        private bool _isOn;

        public bool IsOn
        {
            get => _isOn;
            set
            {
                if (_isOn == value)
                    return;
                _isOn = value;
            }
        }

        public LightColor Color { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
