using System.Diagnostics;
using GoodPass.Presentation.ViewModels;

namespace GoodPass.Presentation;

public partial class MainViewModel : ObservableObject
{
    private INavigator _navigator;

    [ObservableProperty]
    private string? name;

    public MainViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _navigator = navigator;
        Title = "Main";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";
        GoToData = new AsyncRelayCommand(GoToDataPage);
        GoToSettings = new AsyncRelayCommand(GoToSettingsPage);
        Debug.WriteLine(Title);
    }
    public string? Title
    {
        get;
    }

    public ICommand GoToData
    {
        get;
    }

    public ICommand GoToSettings
    {
        get;
    }


    private async Task GoToDataPage()
    {
        await _navigator.NavigateViewModelAsync<DataPageViewModel>(this, data: new Entity(Name!));
    }

    private async Task GoToSettingsPage()
    {
        await _navigator.NavigateViewModelAsync<SettingsPageViewModel>(this, data: new Entity(Name!));
    }
}
