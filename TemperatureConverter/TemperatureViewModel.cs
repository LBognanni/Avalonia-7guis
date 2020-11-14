using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TemperatureConverter
{
    class TemperatureViewModel : INotifyPropertyChanged
    {
        private decimal _celsius;
        private decimal _farenheit;
        private bool _isUpdating;

        public TemperatureViewModel() => Celsius = 0;

        public decimal Celsius
        {
            get => _celsius; 
            set
            {
                if (_celsius != value)
                {
                    _celsius = value;
                    OnPropertyChanged();

                    if (!_isUpdating)
                    {
                        _isUpdating = true;
                        Farenheit = Celsius * (9M / 5M) + 32M;
                        _isUpdating = false;
                    }
                }
            }
        }

        public decimal Farenheit
        {
            get => _farenheit; 
            set
            {
                if (_farenheit != value)
                {
                    _farenheit = value;
                    OnPropertyChanged();

                    if (!_isUpdating)
                    {
                        _isUpdating = true;
                        Celsius = (value - 32M) * (5.0M / 9.0M);
                        _isUpdating = false;
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
