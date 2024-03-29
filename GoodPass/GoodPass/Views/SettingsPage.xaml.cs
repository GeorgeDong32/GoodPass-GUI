﻿using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using GoodPass.Dialogs;
using GoodPass.Helpers;
using GoodPass.Models;
using GoodPass.Services;
using GoodPass.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace GoodPass.Views;

public sealed partial class SettingsPage : Page
{
    #region Properties
    public SettingsViewModel ViewModel
    {
        get;
    }

    public LanguageManager LanguageManager
    {
        get; set; 
    }

    #endregion

    #region Methods
    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        LanguageManager = new LanguageManager();
        Debug.WriteLine(LanguageManager.CurrentLanguage.DisplayName);
        Debug.WriteLine(CultureInfo.CurrentUICulture.DisplayName);
        InitializeComponent();
        if (App.App_IsLock())
        {
            MicrosoftPassportButton.IsEnabled = false;
        }
        else
        {
            MicrosoftPassportButton.IsEnabled = true;
        }
        switch (SecurityStatusHelper.GetMSPassportStatusAsync().Result)
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

    private async void MicrosoftPassportButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton tb)
        {
            switch (tb.IsChecked)
            {
                case true:
                    var dialog = new MicrosoftPassportDialog()
                    {
                        XamlRoot = this.XamlRoot,
                        Style = App.Current.Resources["DefaultContentDialogStyle"] as Style,
                        Title = "启用Microsoft Passport"
                    };
                    var result = await dialog.ShowAsync();
                    if (result == ContentDialogResult.Primary)
                    {
                        var masterKey = dialog.MasterKey;
                        var username = Convert.ToBase64String(Aes.Create().IV);
                        var mpResult = await MicrosoftPassportService.SetMicrosoftPassportAsync(username, masterKey);
                        if (mpResult)
                        {
                            _ = SecurityStatusHelper.SetVaultUsername(username);
                        }
                    }
                    else
                    {
                        tb.IsChecked = false;
                        return;
                    }
                    MicrosoftPassportSituationIcon.Glyph = "\xE73E";
                    MicrosoftPassportSituationText.Text = App.UIStrings.MicrosoftPassportSituatoinText1;
                    _ = await SecurityStatusHelper.SetMSPassportStatusAsync(true);
                    break;
                case false:
                    var dialog1 = new MicrosoftPassportDialog()
                    {
                        XamlRoot = this.XamlRoot,
                        Style = App.Current.Resources["DefaultContentDialogStyle"] as Style,
                        Title = "禁用Microsoft Passport"
                    };
                    var result1 = await dialog1.ShowAsync();
                    if (result1 == ContentDialogResult.Primary)
                    {
                        var masterKey = dialog1.MasterKey;
                        var username = await SecurityStatusHelper.GetVaultUsername();
                        _ = await MicrosoftPassportService.RemoveMicrosoftPassportAsync(username, masterKey);
                    }
                    else
                    {
                        tb.IsChecked = true;
                        return;
                    }
                    MicrosoftPassportSituationIcon.Glyph = "\xE711";
                    MicrosoftPassportSituationText.Text = App.UIStrings.MicrosoftPassportSituatoinText2;
                    _ = await SecurityStatusHelper.SetMSPassportStatusAsync(false);
                    break;
            }
        }
    }

    private void Settings_LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    #endregion
}
