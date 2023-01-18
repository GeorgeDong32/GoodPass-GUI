using Microsoft.UI.Xaml.Controls;
using GoodPass.Services;

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
        if (AddDataDialog_PasswordLengthBox.Text == String.Empty)
        {
            AddDataDialog_PasswordLengthTeachtip.IsOpen = true;
        }
        else
        {
            var genLength = 0;
            var LengthCheck = true;
            try
            {
                genLength = Convert.ToInt32(AddDataDialog_PasswordLengthBox.Text);
            }
            catch (FormatException)
            {
                AddDataDialog_PasswordLengthTeachtip.Title = "输入长度非数字！";
                AddDataDialog_PasswordLengthTeachtip.IsOpen = true;
                LengthCheck = false;
            }
            catch (OverflowException)
            {
                AddDataDialog_PasswordLengthTeachtip.Title = "输入长度超限！";
                AddDataDialog_PasswordLengthTeachtip.IsOpen = true;
                LengthCheck = false;
            }
            if (LengthCheck)
            {
                if (genLength > 0 && genLength <= 24)
                {
                    AddDataDialog_PasswordBox.Password = GoodPassPWGService.RandomPasswordNormal(genLength);
                }
                else
                {
                    AddDataDialog_PasswordLengthTeachtip.Title = "输入长度超限";
                    AddDataDialog_PasswordLengthTeachtip.IsOpen = true;
                }
            }
        }
    }

    private void AddDataDialog_PasswordMode_RandomSpec_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        AddDataDialog_PasswordModeText.Text = AddDataDialog_PasswordMode_RandomSpec.Text;
        if (AddDataDialog_PasswordLengthBox.Text == String.Empty)
        {
            AddDataDialog_PasswordLengthTeachtip.IsOpen = true;
        }
        else
        {
            var genLength = 0;
            var LengthCheck = true;
            try
            {
                genLength = Convert.ToInt32(AddDataDialog_PasswordLengthBox.Text);
            }
            catch (FormatException)
            {
                AddDataDialog_PasswordLengthTeachtip.Title = "输入长度非数字！";
                AddDataDialog_PasswordLengthTeachtip.IsOpen = true;
                LengthCheck = false;
            }
            catch (OverflowException)
            {
                AddDataDialog_PasswordLengthTeachtip.Title = "输入长度超限！";
                AddDataDialog_PasswordLengthTeachtip.IsOpen = true;
                LengthCheck = false;
            }
            if (LengthCheck)
            {
                if (genLength > 0 && genLength <= 24)
                {
                    AddDataDialog_PasswordBox.Password = GoodPassPWGService.RandomPasswordSpec(genLength);
                }
                else
                {
                    AddDataDialog_PasswordLengthTeachtip.Title = "输入长度超限";
                    AddDataDialog_PasswordLengthTeachtip.IsOpen = true;
                }
            }
        }
    }

    private void AddDataDialog_PasswordMode_GPStyle_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        AddDataDialog_PasswordModeText.Text = AddDataDialog_PasswordMode_GPStyle.Text;
        if (AddDataDialog_AccountBox.Text == String.Empty)
        {
            if (AddDataDialog_PlatformBox.Text == String.Empty)
            {
                AddDataDialog_PasswordModeTeachtip.Title = "账户名和平台名不能为空！";
                AddDataDialog_PasswordModeTeachtip.IsOpen = true;
            }
            else if (AddDataDialog_PlatformBox.Text != String.Empty)
            {
                AddDataDialog_PasswordModeTeachtip.Title = "账户名不能为空！";
                AddDataDialog_PasswordModeTeachtip.IsOpen = true;
            }
        }
        else if (AddDataDialog_PlatformBox.Text == String.Empty)
        {
            AddDataDialog_PasswordModeTeachtip.Title = "平台名不能为空！";
            AddDataDialog_PasswordModeTeachtip.IsOpen = true;
        }
        else
        {
            AddDataDialog_PasswordBox.Password = GoodPassPWGService.GPstylePassword(AddDataDialog_PlatformBox.Text, AddDataDialog_AccountBox.Text);
        }
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
