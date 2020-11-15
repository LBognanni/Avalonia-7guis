using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FlightBooker
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ReservationViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
