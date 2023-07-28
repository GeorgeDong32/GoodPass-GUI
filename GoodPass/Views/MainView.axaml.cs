using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GoodPass.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    public async void LoginButtonClick(object source, RoutedEventArgs args)
    {

        await Task.CompletedTask;
    }
}
