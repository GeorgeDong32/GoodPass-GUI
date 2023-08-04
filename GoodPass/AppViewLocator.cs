using System;
using GoodPass.ViewModels;
using GoodPass.Views;
using ReactiveUI;

namespace GoodPass;

public class AppViewLocator : ReactiveUI.IViewLocator
{
    public IViewFor ResolveView<T>(T viewModel, string contract = null) => viewModel switch
    {
        MainViewModel context => new MainView { DataContext = context },
        DataPageViewModel context => new MainView { DataContext = context },
        _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
    };
}
