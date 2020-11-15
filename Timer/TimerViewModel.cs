using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Timer
{
    class TimerViewModel : INotifyPropertyChanged
    {
        private System.Timers.Timer _timer;
        private int _duration;
        private decimal _elapsed;

        public TimerViewModel()
        {
            Duration = 10;
        }

        public int Duration
        {
            get => _duration;
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Elapsed
        {
            get => _elapsed;
            set
            {
                if (_elapsed != value)
                {
                    _elapsed = value;
                    OnPropertyChanged();
                }
            }
        }

        public void Reset()
        {
            Elapsed = 0;
            _timer = new System.Timers.Timer(100);
            _timer.Elapsed += (s, a) =>
            {
                if (Elapsed < Duration)
                {
                    Elapsed += 0.1M;
                }
            };
            _timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
