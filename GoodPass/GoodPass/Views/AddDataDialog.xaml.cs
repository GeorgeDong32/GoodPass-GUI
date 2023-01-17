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
        //Todo:生成对应样式的密码并填充到密码框内
    }

    private void AddDataDialog_PasswordMode_RandomSpec_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        AddDataDialog_PasswordModeText.Text = AddDataDialog_PasswordMode_RandomSpec.Text;
        //Todo:生成对应样式的密码并填充到密码框内
    }

    private void AddDataDialog_PasswordMode_GPStyle_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        AddDataDialog_PasswordModeText.Text = AddDataDialog_PasswordMode_GPStyle.Text;
        //Todo:生成对应样式的密码并填充到密码框内
    }

    private void Add_PasswordRevealButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (Add_PasswordRevealButton.IsChecked == true)
        {
            AddDataDialog_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
        }
        else
        {
            AddDataDialog_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
        }
    }

    private void AddDataDialog_PlatformBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (AddDataDialog_PlatformBox.Text != String.Empty)
        {
            AddDataDialog_PlatformCheckIcon.Glyph = "\xE73E";
            AddDataDialog_PlatformCheckText.Text = "平台名合法";
        }
        else
        {
            AddDataDialog_PlatformCheckIcon.Glyph = "\xE711";
            AddDataDialog_PlatformCheckText.Text = "平台名不能为空";
        }
    }

    private void AddDataDialog_AccountBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (AddDataDialog_AccountBox.Text != String.Empty)
        {
            AddDataDialog_AccountCheckIcon.Glyph = "\xE73E";
            AddDataDialog_AccountCheckText.Text = "平台名合法";
        }
        else
        {
            AddDataDialog_AccountCheckIcon.Glyph = "\xE711";
            AddDataDialog_AccountCheckText.Text = "平台名不能为空";
        }
    }

    private void AddDataDialog_PlatformUrlBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (AddDataDialog_PlatformUrlBox.Text != String.Empty)
        {
            var checkStatus = true;
            try
            {
                var url = new Uri(AddDataDialog_PlatformUrlBox.Text);
            }
            catch (UriFormatException)
            {
                checkStatus = false;
            }
            if (checkStatus)
            {
                AddDataDialog_UrlCheckIcon.Glyph = "\xE73E";
                AddDataDialog_UrlCheckText.Text = "平台Url合法";
            }
            else
            {
                AddDataDialog_UrlCheckIcon.Glyph = "\xE711";
                AddDataDialog_UrlCheckText.Text = "请使用http开头的完整Url格式";
            }
        }
        else
        {
            AddDataDialog_UrlCheckIcon.Glyph = "\xE946";
            AddDataDialog_UrlCheckText.Text = "Url为空，可选择添加链接";
        }
    }

    private void AddDataDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {

    }
}
