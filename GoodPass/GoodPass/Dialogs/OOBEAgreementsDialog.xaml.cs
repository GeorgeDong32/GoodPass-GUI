using GoodPass.Services;
using Microsoft.UI.Xaml.Controls;

namespace GoodPass.Dialogs;

public sealed partial class OOBEAgreementsDialog : ContentDialog
{
    public OOBEAgreementsDialog()
    {
        this.InitializeComponent();
        IsPrimaryButtonEnabled = false;
    }

    private void OOBEAgreementsDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        App.Current.Exit();
    }

    private bool IsAgreeAllChecked()
    {
        if (PrivacyTermsCheck.IsChecked == true && UserAgreementCheck.IsChecked == true)
            return true;
        else
            return false;
    }

    private void OOBEDialogCheck_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (IsAgreeAllChecked())
            IsPrimaryButtonEnabled = true;
    }

    private void OOBEDialogCheck_UnChecked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        IsPrimaryButtonEnabled = false;
    }

    private async void OOBEAgreementsDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        _ = await App.GetService<OOBEServices>().SetOOBEStatusAsync("AgreementOOBE", Models.OOBESituation.DIsableOOBE);
    }
}