using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TemperatureConverter
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.DataContext = new TemperatureViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
