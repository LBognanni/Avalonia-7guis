using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace CircleDrawer
{
    class CirclesViewModel : INotifyPropertyChanged
    {
        const int _defaultRadius = 20;

        public ObservableCollection<Circle> Circles { get; set; }
        private Stack<IEnumerable<Circle>> _undoStack;
        private Stack<IEnumerable<Circle>> _redoStack;

        public CirclesViewModel()
        {
            Circles = new ObservableCollection<Circle>();
            Circles.Add(new Circle { X = 110, Y = 10, Radius = 10 });
            Circles.Add(new Circle { X = 150, Y = 150, Radius = 20 });
            Circles.Add(new Circle { X = 230, Y = 370, Radius = 40 });
            Circles.Add(new Circle { X = 440, Y = 220, Radius = 30 });
            _undoStack = new Stack<IEnumerable<Circle>>();
            _redoStack = new Stack<IEnumerable<Circle>>();
        }

        public void Add(int X, int Y)
        {
            SaveUndo();

            Circles.Add(new Circle { X = X, Y = Y, Radius = _defaultRadius });
            OnPropertyChanged(nameof(Circles));
        }

        public void SaveUndo(bool clearRedo = true)
        {
            if (clearRedo)
            {
                _redoStack.Clear();
                OnPropertyChanged(nameof(CanRedo));
            }

            _undoStack.Push(Circles.Select(c => c.Copy()).ToArray());
            OnPropertyChanged(nameof(CanUndo));
        }

        public void Undo()
        {
            _redoStack.Push(Circles.Select(c => c.Copy()).ToArray());
            Circles = new ObservableCollection<Circle>(_undoStack.Pop());
            OnPropertyChanged(nameof(Circles));
            OnPropertyChanged(nameof(CanRedo));
            OnPropertyChanged(nameof(CanUndo));
        }

        public void Redo()
        {
            SaveUndo(false);
            Circles = new ObservableCollection<Circle>(_redoStack.Pop().ToList());
            OnPropertyChanged(nameof(CanRedo)); ;
            OnPropertyChanged(nameof(Circles));
        }

        public Circle? HandleMouse(int x, int y)
        {
            Circle? selectedCircle = null;

            foreach (var circle in Circles)
            {
                if (circle.HitTest(x, y))
                {
                    circle.Selected = true;
                    selectedCircle = circle;
                }
                else
                {
                    if (circle.Selected)
                    {
                        circle.Selected = false;
                    }
                }
            }

            if(selectedCircle != null)
            {
                return selectedCircle;
            }

            Add(x, y);
            return null;
        }

        public bool CanUndo => _undoStack.Any();
        public bool CanRedo => _redoStack.Any();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
