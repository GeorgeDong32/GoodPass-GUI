using GoodPass.Dialogs;
using GoodPass.Services;
using GoodPass.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace GoodPass.Views;

public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }

    private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Check Microsoft Passport is setup and available on this machine
        if (await MicrosoftPassportHelper.MicrosoftPassportAvailableCheckAsync())
        {
            TestButton.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(100, 98, 255, 223));
        }
        else
        {
            TestButton.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(200, 255, 0, 0));
        }
    }
}
