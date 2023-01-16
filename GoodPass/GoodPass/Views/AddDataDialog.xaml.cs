using Microsoft.UI.Xaml.Controls;

namespace GoodPass.Views;

public sealed partial class AddDataDialog : ContentDialog
{
    public AddDataDialog()
    {
        this.InitializeComponent();
    }

    private void AddDataDialog_PasswordMode_RandomNoSpec_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        AddDataDialog_PasswordModeText.Text = AddDataDialog_PasswordMode_RandomNoSpec.Text;
    }

    private void AddDataDialog_PasswordMode_RandomSpec_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        AddDataDialog_PasswordModeText.Text = AddDataDialog_PasswordMode_RandomSpec.Text;
    }

    private void AddDataDialog_PasswordMode_GPStyle_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        AddDataDialog_PasswordModeText.Text = AddDataDialog_PasswordMode_GPStyle.Text;
    }

    private void Add_PasswordRevealButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (Add_PasswordRevealButton.IsChecked == true)
            AddDataDialog_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
        else
            AddDataDialog_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
    }
}
