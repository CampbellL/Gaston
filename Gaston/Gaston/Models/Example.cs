using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Gaston.Annotations;

namespace Gaston.Models
{
    public abstract class Example :  INotifyPropertyChanged
    {
        public int Score;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}