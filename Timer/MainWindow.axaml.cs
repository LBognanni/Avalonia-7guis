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
#if DEBUG
            this.AttachDevTools();
#endif
            this.DataContext = new TimerViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
