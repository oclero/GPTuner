using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Tuner.FrameworkMVVM
{
    public class BaseUserControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool NotifyPropertyChanged<T>(ref T variable, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(variable, value)) return false;

            variable = value;
            NotifyPropertyChanged(propertyName);
            return true;
        }
    }
}
