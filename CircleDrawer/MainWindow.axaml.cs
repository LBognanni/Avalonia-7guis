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
            var selected = _model.HandleMouse((int)pos.X, (int)pos.Y);
            if(selected!=null)
            {
                var oldRadius = selected.Radius;

                var detailsWindow = new CircleDetailsWindow();
                detailsWindow.DataContext = selected;

                int x = this.Position.X + (int)(this.Width / 2) - (int)(detailsWindow.Width / 2);
                int y = this.Position.Y + (int)this.Height - (int)detailsWindow.Height;

                detailsWindow.Position = new PixelPoint(x, y);

                _model.SaveUndo();
                detailsWindow.ShowDialog(this);
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
