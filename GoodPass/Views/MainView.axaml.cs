using System.Threading.Tasks;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using GoodPass.ViewModels;
using ReactiveUI;

namespace GoodPass.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
        InitializeComponent();
    }

    public async void LoginButtonClick(object source, RoutedEventArgs args)
    {

        await Task.CompletedTask;
    }
}