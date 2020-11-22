using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CircleDrawer
{
    public class CircleDetailsWindow : Window
    {
        public CircleDetailsWindow()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
