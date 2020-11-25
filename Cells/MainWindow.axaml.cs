using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Cells
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new GridViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
