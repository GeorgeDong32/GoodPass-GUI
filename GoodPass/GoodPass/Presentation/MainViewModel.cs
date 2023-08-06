using System.Diagnostics;

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
        GoToSecond = new AsyncRelayCommand(GoToDataPage);
        Debug.WriteLine(Title);
    }
    public string? Title
    {
        get;
    }

    public ICommand GoToSecond
    {
        get;
    }


    private async Task GoToDataPage()
    {
        await _navigator.NavigateViewModelAsync<DataPageViewModel>(this, data: new Entity(Name!));
    }

}
