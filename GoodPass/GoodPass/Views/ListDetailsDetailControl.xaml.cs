using GoodPass.Models;
using GoodPass.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;

namespace GoodPass.Views;

public sealed partial class ListDetailsDetailControl : UserControl
{
    public ListDetailsViewModel ViewModel
    {
        get;
    }

    public GPData? ListDetailsMenuItem
    {

        get => GetValue(ListDetailsMenuItemProperty) as GPData;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }
    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(GPData), typeof(ListDetailsDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public ListDetailsDetailControl()
    {
        ViewModel = App.GetService<ListDetailsViewModel>();
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ListDetailsDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }

    private void ListDetailsDetailControl_PasswordCopyButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var password = ListDetailsDetailControl_PasswordBox.Password;
        var passwordDatapackage = new DataPackage();
        passwordDatapackage.SetText(password);
        Clipboard.SetContent(passwordDatapackage);
        CopiedTipforPasswordCopyButton.IsOpen = true;
    }

    private void ListDetailsDetailControl_AcconutNameCopyButton_Click(object sender, RoutedEventArgs e)
    {
        var accountName = ListDetailsDetailControl_AccountNameText.Text;
        var dataPackage = new DataPackage();
        dataPackage.SetText(accountName);
        Clipboard.SetContent(dataPackage);
        CopiedTipforAcconutNameCopyButton.IsOpen = true;
    }

    private void PasswordRevealButton_Click(object sender, RoutedEventArgs e)
    {
        if (PasswordRevealButton.IsChecked == true)
            ListDetailsDetailControl_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
        else
            ListDetailsDetailControl_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;

    }

    private void ListDetailsDetailControl_EditButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void ListDetailsDetailControl_DeleteButton_Click(object sender, RoutedEventArgs e)
    {

    }
}
