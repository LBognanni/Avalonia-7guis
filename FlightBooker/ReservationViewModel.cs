using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace FlightBooker
{
    public enum ReservationType
    {
        OneWay,
        Return
    }

    class ReservationViewModel : INotifyPropertyChanged
    {
        private DateTime _date1;
        private DateTime _date2;
        private KeyValuePair<ReservationType, string> _selectedReservationType;

        public ReservationViewModel()
        {
            _date1 = DateTime.Today;
            _date2 = DateTime.Today.AddDays(-1);
            ReservationTypes = new ObservableCollection<KeyValuePair<ReservationType, string>>(new Dictionary<ReservationType, string>
            {
                { ReservationType.OneWay, "one-way flight" },
                { ReservationType.Return, "return flight" }
            });

            SelectedReservationType = ReservationTypes.First();
        }

        public string Date1
        {
            get => _date1.ToShortDateString();
            set
            {
                var newDate = DateTime.Parse(value);
                if (_date1 != newDate)
                {
                    _date1 = newDate;
                    OnAllPropertiesChanged();
                }
            }
        }

        public string Date2
        {
            get => _date2.ToShortDateString();
            set
            {
                var newDate = DateTime.Parse(value);
                if (_date2 != newDate)
                {
                    _date2 = newDate;
                    OnAllPropertiesChanged();
                }
            }
        }

        public bool Date2Enabled => SelectedReservationType.Key == ReservationType.Return;

        public KeyValuePair<ReservationType, string> SelectedReservationType
        {
            get => _selectedReservationType;
            set
            {
                _selectedReservationType = value;
                OnAllPropertiesChanged();
            }
        }


        public ObservableCollection<KeyValuePair<ReservationType, string>> ReservationTypes { get; }

        public void Book()
        {
            // call an API or something
        }

        public bool CanBook
        {
            get
            {
                if (Date2Enabled)
                {
                    return _date2 > _date1;
                }
                return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnAllPropertiesChanged() => OnPropertyChanged("");
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
