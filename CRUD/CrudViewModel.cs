using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CRUD
{
    class CrudViewModel : INotifyPropertyChanged
    {
        private string _filter;
        private List<Person> _people;
        private Person _selectedPerson;
        private Person _editingPerson;

        public CrudViewModel()
        {
            _people = new List<Person>();
            _people.Add(new Person { Name = "Hans", Surname = "Emil" });
            _people.Add(new Person { Name = "Max", Surname = "Mustermann" });
            _people.Add(new Person { Name = "Roman", Surname = "Tisch" });
            _editingPerson = new Person();
            _filter = "";
        }

        public string Filter
        {
            get => _filter;
            set
            {
                if (_filter != value)
                {
                    _filter = value;

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(People));
                }
            }
        }

        public IEnumerable<Person> People => _people.Where(p => p.FullName.Contains(_filter, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(_filter));

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                if (_selectedPerson != value)
                {
                    _selectedPerson = value;

                    if (value == null)
                    {
                        EditingPerson = new Person();
                    }
                    else
                    {
                        EditingPerson = new Person { Name = value.Name, Surname = value.Surname };
                    }
                    OnPropertyChanged();
                    RefreshButtons();
                }
            }
        }

        private void RefreshButtons()
        {
            OnPropertyChanged(nameof(CanCreate));
            OnPropertyChanged(nameof(CanUpdate));
            OnPropertyChanged(nameof(CanDelete));
        }

        public Person EditingPerson
        {
            get => _editingPerson;
            set
            {
                if (!_editingPerson.Equals(value))
                {
                    _editingPerson = value;
                    OnPropertyChanged();
                    _editingPerson.PropertyChanged += (s, e) => RefreshButtons();
                }
            }
        }

        public void Create()
        {
            _people.Add(_editingPerson);
            EditingPerson = new Person();
            OnPropertyChanged(nameof(People));
        }

        public void Update()
        {
            var idx = _people.IndexOf(_selectedPerson);
            _people[idx] = _editingPerson;
            EditingPerson = new Person();
            SelectedPerson = null;
            OnPropertyChanged(nameof(People));
        }

        public void Delete()
        {
            _people.Remove(_selectedPerson);
            SelectedPerson = null;
            OnPropertyChanged(nameof(People));
        }

        public bool CanUpdate => _selectedPerson != null && _editingPerson != null && _editingPerson.IsFilled && !_selectedPerson.Equals(_editingPerson);
        public bool CanDelete => _selectedPerson != null;
        public bool CanCreate => _editingPerson != null && _editingPerson.IsFilled;


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
