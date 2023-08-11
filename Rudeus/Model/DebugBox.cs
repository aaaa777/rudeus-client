using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Model
{
    /// <summary>
    /// singleton class
    /// binding string
    /// </summary>
    internal class DebugBox : INotifyPropertyChanged
    {
        private static DebugBox Instanse = null;

        private DebugBox() { }

        public static DebugBox Load()
        {
            if (Instanse == null)
            {
                Instanse = new DebugBox();
            }
            return Instanse;
        }

        private string _lastText = "sample text data";
        public string LastText
        {
            get => _lastText;
            set
            {
                _lastText = value;
                OnPropertyChanged(nameof(LastText));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
