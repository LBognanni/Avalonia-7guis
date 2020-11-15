using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CRUD
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new CrudViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
