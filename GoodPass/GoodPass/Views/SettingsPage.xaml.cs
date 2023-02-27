using GoodPass.Dialogs;
using GoodPass.Helpers;
using GoodPass.Services;
using GoodPass.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Windows.Security.Credentials;

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
        MicrosoftPassportButton.Content = App.UIStrings.MicrosoftPassportButtonText1;
        switch (SecurityStatusHelper.GetMSPassportStatus().Result)
        {
            case true:
                MicrosoftPassportButton.IsChecked = true;
                MicrosoftPassportSituationIcon.Glyph = "\xE73E";
                MicrosoftPassportSituationText.Text = App.UIStrings.MicrosoftPassportSituatoinText1;
                break;
            case false:
                MicrosoftPassportButton.IsChecked = false;
                MicrosoftPassportSituationIcon.Glyph = "\xE711";
                MicrosoftPassportSituationText.Text = App.UIStrings.MicrosoftPassportSituatoinText2;
                break;
        }
    }

    private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        // Check Microsoft Passport is setup and available on this machine
        if (await MicrosoftPassportHelper.MicrosoftPassportAvailableCheckAsync())
        {
            TestButton.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(100, 98, 255, 223));
            var openKeyResult = await KeyCredentialManager.OpenAsync("2572593789@qq.com");

            if (openKeyResult.Status == KeyCredentialStatus.Success)
            {
                var userKey = openKeyResult.Credential;
                var publicKey = userKey.RetrievePublicKey();
                var signResult = await KeyCredentialManager.OpenAsync("2572593789@qq.com");

                if (signResult.Status == KeyCredentialStatus.Success)
                {
                    var dialog = new GPDialog2()
                    {
                        XamlRoot = this.XamlRoot,
                        Style = App.Current.Resources["DefaultContentDialogStyle"] as Style,
                    };
                    dialog.Content = "验证成功";
                    dialog.Title = "Test";
                    _ = await dialog.ShowAsync();
                }
                else if (signResult.Status == KeyCredentialStatus.UserPrefersPassword)
                {

                }
            }
        }
        else
        {
            TestButton.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(200, 255, 0, 0));
        }
    }

    private void MicrosoftPassportButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton tb)
        {
            switch(tb.IsChecked)
            {
                case true:
                    MicrosoftPassportButton.Content = App.UIStrings.MicrosoftPassportButtonText2;
                    //TODO: 取消Microsoft Passport关联
                    MicrosoftPassportSituationIcon.Glyph = "\xE73E";
                    MicrosoftPassportSituationText.Text = App.UIStrings.MicrosoftPassportSituatoinText1;
                    break;
                case false:
                    MicrosoftPassportButton.Content = App.UIStrings.MicrosoftPassportButtonText1;
                    //TODO: 关联Microsoft Passport
                    MicrosoftPassportSituationIcon.Glyph = "\xE711";
                    MicrosoftPassportSituationText.Text = App.UIStrings.MicrosoftPassportSituatoinText2;
                    break;
            }
        }
    }
}
