using GoodPass.Dialogs;
using GoodPass.Models;
using GoodPass.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;
using System.Runtime.CompilerServices;

namespace GoodPass.Views;

public sealed partial class ListDetailsDetailControl : UserControl
{
    public GPData? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as GPData;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }
    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(GPData), typeof(ListDetailsDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public ListDetailsDetailControl()
    {
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ListDetailsDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }

    /// <summary>
    /// 复制密码到Windows剪贴板
    /// </summary>
    private void ListDetailsDetailControl_PasswordCopyButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var password = App.DataManager.GetData(PlatformNameText.Text, ListDetailsDetailControl_AccountNameText.Text).GetPassword;
        var passwordDatapackage = new DataPackage();
        passwordDatapackage.SetText(password);
        Clipboard.SetContent(passwordDatapackage);
        CopiedTipforPasswordCopyButton.Title = App.UIStrings.ListDetailsDetailControl_CopiedTipforPasswordCopyButtonTitle;
        CopiedTipforPasswordCopyButton.IsOpen = true;
    }

    /// <summary>
    /// 复制账号到Windows剪贴板
    /// </summary>
    private void ListDetailsDetailControl_AcconutNameCopyButton_Click(object sender, RoutedEventArgs e)
    {
        var accountName = ListDetailsDetailControl_AccountNameText.Text;
        var dataPackage = new DataPackage();
        dataPackage.SetText(accountName);
        Clipboard.SetContent(dataPackage);
        CopiedTipforAcconutNameCopyButton.Title = App.UIStrings.ListDetailsDetailControl_CopiedTipforAcconutNameCopyButtonTitle;
        CopiedTipforAcconutNameCopyButton.IsOpen = true;
    }

    /// <summary>
    /// 显示PasswordBox中的密码
    /// </summary>
    private void PasswordRevealButton_Click(object sender, RoutedEventArgs e)
    {
        var password = App.DataManager.GetData(PlatformNameText.Text, ListDetailsDetailControl_AccountNameText.Text).GetPassword;
        if (PasswordRevealButton.IsChecked == true)
        {
            ListDetailsDetailControl_PasswordBox.Password = password;
            ListDetailsDetailControl_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
        }
        else
        {
            ListDetailsDetailControl_PasswordBox.Password = password;
            ListDetailsDetailControl_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
        }
    }

    /// <summary>
    /// 进入编辑模式
    /// </summary>
    private async void ListDetailsDetailControl_EditButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new EditDataDialog(ListDetailsDetailControl_AccountNameText.Text, PlatformNameText.Text, ListDetailsDetailControl_PlatformUrlHyperLinkText.Text, App.DataManager.GetData(PlatformNameText.Text, ListDetailsDetailControl_AccountNameText.Text).GetPassword)
        {
            XamlRoot = this.XamlRoot,
            Style = App.Current.Resources["DefaultContentDialogStyle"] as Style
        };
        _ = await dialog.ShowAsync();
        if (dialog.Result == EditDataResult.Nochange)
        {
            var warningdialog = new GPDialog2()
            {
                XamlRoot = this.XamlRoot,
                Style = App.Current.Resources["DefaultContentDialogStyle"] as Style,
                Title = "提示",
                Content = "数据没有任何修改！"
            };
            _ = warningdialog.ShowAsync();
        }
        else if (dialog.Result == EditDataResult.Success)
        {
            ListDetailsDetailControl_AccountNameText.Text = dialog.newAccountName;
            ListDetailsDetailControl_PasswordBox.Password = dialog.newPassword;
            ListDetailsDetailControl_PlatformUrlHyperLinkText.Text = dialog.newPlatformUrl;
            PlatformNameText.Text = dialog.newPlatformName;
            ListDetailsDetailControl_LastmodifiedText.Text = dialog.newDateTime.ToString();
            App.DataManager.SaveToFile($"C:\\Users\\{Environment.UserName}\\AppData\\Local\\GoodPass\\GoodPassData.csv");
        }
        else if (dialog.Result == EditDataResult.Failure)
        {
            var warningdialog = new GPDialog2()
            {
                XamlRoot = this.XamlRoot,
                Style = App.Current.Resources["DefaultContentDialogStyle"] as Style,
                Title = "出错了！",
                Content = "编辑数据过程中出错，请重试或者联系开发者"
            };
            _ = warningdialog.ShowAsync();
        }
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    private async void ListDetailsDetailControl_DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        //弹窗提示用户确认
        var dialog = new GPDialog2();
        dialog.XamlRoot = this.XamlRoot;
        dialog.Style = App.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = "删除确认";
        dialog.Content = "您确定要删除这个数据吗？该操作不可撤销！";
        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            var tarPlatform = ListDetailsMenuItem.PlatformName;
            var tarAccountName = ListDetailsMenuItem.AccountName;
            try
            {
                App.ListDetailsVM.DeleteDataItem(App.DataManager.GetData(tarPlatform, tarAccountName));
                var delResult = App.DataManager.DeleteData(tarPlatform, tarAccountName);
                if (delResult == false)
                    throw new GPObjectNotFoundException("Data Not Found!");
                else
                    App.DataManager.SaveToFile($"C:\\Users\\{Environment.UserName}\\AppData\\Local\\GoodPass\\GoodPassData.csv");
            }
            catch (System.ArgumentOutOfRangeException)
            {
                var warningDialog = new GPDialog2();
                warningDialog.XamlRoot = XamlRoot;
                warningDialog.Style = App.Current.Resources["DefaultContentDialogStyle"] as Style;
                warningDialog.Title = "出错了！";
                warningDialog.Content = "您试图删除一个不存在的对象";
                _ = await warningDialog.ShowAsync();
            }
            catch (GPObjectNotFoundException)
            {
                var warningDialog = new GPDialog2();
                warningDialog.XamlRoot = XamlRoot;
                warningDialog.Style = App.Current.Resources["DefaultContentDialogStyle"] as Style;
                warningDialog.Title = "出错了！";
                warningDialog.Content = "您试图删除一个不存在的对象";
                _ = await warningDialog.ShowAsync();
            }
        }
    }

    /// <summary>
    /// 超链接按钮点击功能
    /// </summary>
    private async void ListDetailsDetailControl_PlatformUrlHyperLink_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ListDetailsDetailControl_PlatformUrlHyperLink.NavigateUri = new Uri(ListDetailsMenuItem.PlatformUrl);
        }
        catch (ArgumentNullException)
        {
            ListDetailsDetailControl_PlatformUrlHyperLink.NavigateUri = null;
            var warningDialog = new GPDialog2();
            warningDialog.XamlRoot = XamlRoot;
            warningDialog.Style = App.Current.Resources["DefaultContentDialogStyle"] as Style;
            warningDialog.Title = "出错了！";
            warningDialog.Content = "链接为空，无法访问！";
            _ = await warningDialog.ShowAsync();
        }
        catch (UriFormatException)
        {
            ListDetailsDetailControl_PlatformUrlHyperLink.NavigateUri = null;
            var warningDialog = new GPDialog2();
            warningDialog.XamlRoot = XamlRoot;
            warningDialog.Style = App.Current.Resources["DefaultContentDialogStyle"] as Style;
            warningDialog.Title = "出错了！";
            warningDialog.Content = "链接为空，无法访问！";
            _ = await warningDialog.ShowAsync();
        }
    }

    public void ShowOOBETips()
    {
        this.OOBE_DeleteTip.IsOpen = true;
        this.OOBE_EditTip.IsOpen = true;
        this.CopiedTipforPasswordCopyButton.Title = "点击此按钮复制账号名";
        this.CopiedTipforAcconutNameCopyButton.Title = "点击此按钮复制密码";
        this.CopiedTipforAcconutNameCopyButton.IsOpen = true;
        this.CopiedTipforPasswordCopyButton.IsOpen = true;
    }
}
