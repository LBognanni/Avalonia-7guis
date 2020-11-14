using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TemperatureConverter
{
    class TemperatureViewModel : INotifyPropertyChanged
    {
        public TemperatureViewModel() => Celsius = 0;

        private decimal _celsius;
        public decimal Celsius
        {
            get => _celsius; 
            set
            {
                if (_celsius != value)
                {
                    _celsius = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Farenheit));
                }
            }
        }

        public decimal Farenheit
        {
            get => Celsius * (9M / 5M) + 32M;
            set
            {
                Celsius = (value - 32M) * (5.0M / 9.0M);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
