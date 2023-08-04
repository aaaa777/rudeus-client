using rudeus_client.View;
using rudeus_client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace rudeus_client.ViewModel
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        

        public string Text  { get => $"AccessToken: {Device.AccessToken}"; }
        public string Text2 { get => $"AccessToken: {Device.AccessToken}"; }
        public string Text3 { get => $"AccessToken: {Device.AccessToken}"; }

        public Model.Device Device { get; set; } = new("", "", "");

        public BaseCommand RegisterDeviceCommand { get; set; }
        public BaseCommand UpdateDeviceCommand { get; set; }
        public BaseCommand LoginDeviceCommand { get; set; }

        public MainPageViewModel()
        {
            RegisterDeviceCommand = new (() =>
            {
                Device = new("test_id", "HIU-P123", "pc");
                Device.Register();
                OnPropertyChanged(nameof(Text));
            });

            UpdateDeviceCommand = new (() =>
            {
                Device.Update();
                OnPropertyChanged(nameof(Text3));
            });

            LoginDeviceCommand = new (() =>
            {
                Device.Login();
                OnPropertyChanged(nameof(Text2));
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

