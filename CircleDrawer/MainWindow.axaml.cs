using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace CircleDrawer
{
    public class MainWindow : Window
    {
        private CirclesViewModel _model;
        public MainWindow()
        {
            InitializeComponent();
            _model = new CirclesViewModel();
            this.DataContext = _model;
        }

        private void MainWindow_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            var pos = e.GetPosition((Control)sender);
            _model.HandleMouse((int)pos.X, (int)pos.Y);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
