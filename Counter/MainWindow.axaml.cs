﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Counter
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new CounterViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
