using System.ComponentModel;
using System.Runtime.CompilerServices;
using RandomApp.Client.Annotations;

namespace RandomApp.Client.Models
{
    public class RandomModel : INotifyPropertyChanged
    {
        private double number;

        public double Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
