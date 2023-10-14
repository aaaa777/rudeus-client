using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RudeusBgForm
{
    internal class LoginUserModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        bool SetProperty<T>(ref T prop, T value, [CallerMemberName] string? name = null)
        {
            if (object.Equals(prop, value)) return false;
            prop = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            return true;
        }

        string? _loginStatus = "ログインしていません";
        public string? LoginStatus { get => _loginStatus; set { SetProperty(ref _loginStatus, value); } }
    }
}
