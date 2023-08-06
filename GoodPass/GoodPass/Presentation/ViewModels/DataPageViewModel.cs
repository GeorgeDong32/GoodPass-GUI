namespace GoodPass.Presentation;

public partial class DataPageViewModel : ObservableObject
{
    private INavigator _navigator;

    [ObservableProperty]
    private string? name;

    public DataPageViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator)
    {
        _navigator = navigator;
        GoToSettings = new AsyncRelayCommand(GoToSettingsPage);
    }

    public ICommand GoToSettings
    {
        get;
    }

    private async Task GoToSettingsPage()
    {
        await _navigator.NavigateViewModelAsync<SettingsPageViewModel>(this);
    }
}
