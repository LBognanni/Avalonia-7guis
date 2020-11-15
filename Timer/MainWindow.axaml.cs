using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Timer
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new TimerViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
