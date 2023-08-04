using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using GoodPass.ViewModels;
using ReactiveUI;

namespace GoodPass.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
        InitializeComponent();
    }
}
