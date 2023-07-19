using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GoodPass.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    public void ButtonClick(object source, RoutedEventArgs args)
    {
        Button1.Content = "Clicked";
    }
}
