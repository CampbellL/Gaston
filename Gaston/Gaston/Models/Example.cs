using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Gaston.Annotations;

namespace Gaston.Models
{
    public class Example :  INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}