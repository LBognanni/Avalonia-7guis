using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CircleDrawer
{
    class Circle : INotifyPropertyChanged
    {
        private int _left;
        private int _top;
        private int _width;
        private int _height;
        private bool _selected;

        public int X { get => Left + (Width / 2); set => Left = value - (Width / 2); }
        public int Y { get => Top + (Height / 2); set => Top = value - (Height / 2); }
        public int Radius
        {
            get => Height / 2;
            set
            {
                int x = X, y = Y, r = value;
                Left = x - r;
                Top = y - r;
                Width = r * 2;
                Height = r * 2;
            }
        }

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Fill));
            }
        }

        public int Left
        {
            get => _left;
            set
            {
                if (_left != value)
                {
                    _left = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Top
        {
            get => _top;
            set
            {
                if (_top != value)
                {
                    _top = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Width
        {
            get => _width;
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged();
                }
            }
        }

        public Circle Copy() => new Circle { Width = Width, Height = Height, Top = Top, Left = Left };

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        internal bool HitTest(int x, int y)
        {
            var r = Radius;
            var dist = Math.Pow(x - X, 2) + Math.Pow(y - Y, 2);
            return dist < (r * r);
        }

        public IBrush Fill
        {
            get
            {
                if (Selected)
                    return new SolidColorBrush(Color.FromRgb(255, 255, 255));
                return new SolidColorBrush(Color.FromRgb(128, 128, 128));
            }
        }
    }
}
