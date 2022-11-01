using Microsoft.UI.Xaml.Controls;
using GoodPass.Services;
using Microsoft.UI.Xaml;

namespace GoodPass.Views;

public sealed partial class SetMKDialog : ContentDialog
{
    public bool _isComplete { get; set; }

    public SetMKDialog()
    {
        _isComplete = false;
        this.InitializeComponent();
    }

    /*To Do: 设置密码相关逻辑代码*/
    private void RevealModeCheckbox_Changed1(object sender, RoutedEventArgs e)
    {
        if (SetMKDialog_PB1CheckBox.IsChecked == true)
        {
            SetMKDialog_PssswordBox1.PasswordRevealMode = PasswordRevealMode.Visible;
        }
        else
        {
            SetMKDialog_PssswordBox1.PasswordRevealMode = PasswordRevealMode.Hidden;
        }
    }

    private void RevealModeCheckbox_Changed2(object sender, RoutedEventArgs e)
    {
        if (SetMKDialog_PB2CheckBox.IsChecked == true)
        {
            SetMKDialog_PssswordBox2.PasswordRevealMode = PasswordRevealMode.Visible;
        }
        else
        {
            SetMKDialog_PssswordBox2.PasswordRevealMode = PasswordRevealMode.Hidden;
        }
    }

    private void SetMKDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        var MK1 = SetMKDialog_PssswordBox2.Password;
        var MK2 = SetMKDialog_PssswordBox2.Password;
        if (MK1 != MK2 && MK1 != null && MK2 != null)
        {
            SetMKDialog_InfoBar.IsOpen=true;
            SetMKDialog_InfoBar.Message = "两次密码不一致，请检查！";
            //保持弹窗不关闭
        }
    }
}