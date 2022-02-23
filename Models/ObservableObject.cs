using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenStates.Models
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
        {
            PropertyChanged?.Invoke(this, eventArgs);
        }

        /*protected bool SetProperty<T>(ref T storage, T value, PropertyChangedEventArgs args)
        {
            // Do not update if value is equal to the cached one
            if (Equals(storage, value))
                return false;

            // Cache the new value and notify the change
            storage = value;
            OnPropertyChanged(args);

            return true;
        }*/
    }
}
