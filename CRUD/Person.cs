using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace CRUD
{
    public class Person : INotifyPropertyChanged, IEquatable<Person>
    {
        private string _name;
        private string _surname;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FullName => $"{Surname}, {Name}";

        public bool IsFilled => (!string.IsNullOrEmpty(Name)) && (!string.IsNullOrEmpty(Surname));

        public event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            if(obj is Person person)
            {
                return Equals(person);
            }

            return base.Equals(obj);
        }

        public bool Equals([AllowNull] Person other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Name == this.Name && other.Surname == this.Surname;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
