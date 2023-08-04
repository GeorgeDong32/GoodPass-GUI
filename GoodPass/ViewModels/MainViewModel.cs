using System;
using ReactiveUI;

namespace GoodPass.ViewModels;

public class MainViewModel : ReactiveObject, IRoutableViewModel
{
    // Reference to IScreen that owns the routable view model.
    public IScreen HostScreen
    {
        get;
    }

    // Unique identifier for the routable view model.
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    public MainViewModel(IScreen screen) => HostScreen = screen;
}

