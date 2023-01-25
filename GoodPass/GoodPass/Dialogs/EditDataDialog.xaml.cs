using GoodPass.Models;
using GoodPass.Services;
using GoodPass.ViewModels;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.UI.Xaml.Controls;

namespace GoodPass.Dialogs;

public sealed partial class EditDataDialog : ContentDialog
{
    public EditDataResult Result
    {
        get; set;
    }

    private readonly string oldAccountName;
    private readonly string oldPlatformName;
    private readonly string oldPassword;
    private readonly string? oldPlatformUrl;

    public string newAccountName;
    public string newPlatformName;
    public string newPassword;
    public string? newPlatformUrl;

    public EditDataDialog(string accountName, string platformName, string platformUrl, string password)
    {
        this.InitializeComponent();
        IsPrimaryButtonEnabled = true;
        Result = EditDataResult.Failure;
        EditDataDialog_PlatformBox.Text = platformName;
        EditDataDialog_AccountBox.Text = accountName;
        EditDataDialog_PasswordBox.Password = password;
        EditDataDialog_PlatformUrlBox.Text = platformUrl;
        oldAccountName = accountName;
        oldPlatformName = platformName;
        oldPassword = password;
        oldPlatformUrl = platformUrl;
        newAccountName = accountName;
        newPlatformName = platformName;
        newPassword = password;
        newPlatformUrl = platformUrl;
        if (platformUrl != String.Empty)
        {
            EditDataDialog_UrlCheckIcon.Glyph = "\xE73E";
            EditDataDialog_UrlCheckText.Text = "平台Url合法";
        }
    }

    private void EditDataDialog_PasswordMode_RandomNoSpec_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        EditDataDialog_PasswordModeText.Text = EditDataDialog_PasswordMode_RandomNoSpec.Text;
        if (EditDataDialog_PasswordLengthBox.Text == String.Empty)
        {
            EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
        }
        else
        {
            var genLength = 0;
            var LengthCheck = true;
            try
            {
                genLength = Convert.ToInt32(EditDataDialog_PasswordLengthBox.Text);
            }
            catch (FormatException)
            {
                EditDataDialog_PasswordLengthTeachtip.Title = "输入长度非数字！";
                EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
                LengthCheck = false;
            }
            catch (OverflowException)
            {
                EditDataDialog_PasswordLengthTeachtip.Title = "输入长度超限！";
                EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
                LengthCheck = false;
            }
            if (LengthCheck)
            {
                if (genLength > 0 && genLength <= 24)
                {
                    EditDataDialog_PasswordBox.Password = GoodPassPWGService.RandomPasswordNormal(genLength);
                }
                else
                {
                    EditDataDialog_PasswordLengthTeachtip.Title = "输入长度超限";
                    EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
                }
            }
        }
    }

    private void EditDataDialog_PasswordMode_RandomSpec_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        EditDataDialog_PasswordModeText.Text = EditDataDialog_PasswordMode_RandomSpec.Text;
        if (EditDataDialog_PasswordLengthBox.Text == String.Empty)
        {
            EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
        }
        else
        {
            var genLength = 0;
            var LengthCheck = true;
            try
            {
                genLength = Convert.ToInt32(EditDataDialog_PasswordLengthBox.Text);
            }
            catch (FormatException)
            {
                EditDataDialog_PasswordLengthTeachtip.Title = "输入长度非数字！";
                EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
                LengthCheck = false;
            }
            catch (OverflowException)
            {
                EditDataDialog_PasswordLengthTeachtip.Title = "输入长度超限！";
                EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
                LengthCheck = false;
            }
            if (LengthCheck)
            {
                if (genLength > 0 && genLength <= 24)
                {
                    EditDataDialog_PasswordBox.Password = GoodPassPWGService.RandomPasswordSpec(genLength);
                }
                else
                {
                    EditDataDialog_PasswordLengthTeachtip.Title = "输入长度超限";
                    EditDataDialog_PasswordLengthTeachtip.IsOpen = true;
                }
            }
        }
    }

    private void EditDataDialog_PasswordMode_GPStyle_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        EditDataDialog_PasswordModeText.Text = EditDataDialog_PasswordMode_GPStyle.Text;
        if (EditDataDialog_AccountBox.Text == String.Empty)
        {
            if (EditDataDialog_PlatformBox.Text == String.Empty)
            {
                EditDataDialog_PasswordModeTeachtip.Title = "账户名和平台名不能为空！";
                EditDataDialog_PasswordModeTeachtip.IsOpen = true;
            }
            else if (EditDataDialog_PlatformBox.Text != String.Empty)
            {
                EditDataDialog_PasswordModeTeachtip.Title = "账户名不能为空！";
                EditDataDialog_PasswordModeTeachtip.IsOpen = true;
            }
        }
        else if (EditDataDialog_PlatformBox.Text == String.Empty)
        {
            EditDataDialog_PasswordModeTeachtip.Title = "平台名不能为空！";
            EditDataDialog_PasswordModeTeachtip.IsOpen = true;
        }
        else
        {
            EditDataDialog_PasswordBox.Password = GoodPassPWGService.GPstylePassword(EditDataDialog_PlatformBox.Text, EditDataDialog_AccountBox.Text);
        }
    }

    private void Add_PasswordRevealButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (Add_PasswordRevealButton.IsChecked == true)
        {
            EditDataDialog_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible;
        }
        else
        {
            EditDataDialog_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden;
        }
    }

    private void EditDataDialog_PlatformBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (EditDataDialog_PlatformBox.Text != String.Empty)
        {
            EditDataDialog_PlatformCheckIcon.Glyph = "\xE73E";
            EditDataDialog_PlatformCheckText.Text = "平台名合法";
            if (EditDataCheck())
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }
        else
        {
            EditDataDialog_PlatformCheckIcon.Glyph = "\xE711";
            EditDataDialog_PlatformCheckText.Text = "平台名不能为空";
            IsPrimaryButtonEnabled = false;
        }
    }

    private void EditDataDialog_AccountBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (EditDataDialog_AccountBox.Text != String.Empty)
        {
            EditDataDialog_AccountCheckIcon.Glyph = "\xE73E";
            EditDataDialog_AccountCheckText.Text = "平台名合法";
            if (EditDataCheck())
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }
        else
        {
            EditDataDialog_AccountCheckIcon.Glyph = "\xE711";
            EditDataDialog_AccountCheckText.Text = "平台名不能为空";
            IsPrimaryButtonEnabled = false;
        }
    }

    private void EditDataDialog_PasswordBox_PasswordChanged(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (EditDataDialog_PasswordBox.Password != String.Empty)
        {
            if (EditDataCheck())
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }
        else
        {
            IsPrimaryButtonEnabled = false;
        }
    }

    private void EditDataDialog_PlatformUrlBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (EditDataDialog_PlatformUrlBox.Text != String.Empty)
        {
            var checkStatus = true;
            try
            {
                var url = new Uri(EditDataDialog_PlatformUrlBox.Text);
            }
            catch (UriFormatException)
            {
                checkStatus = false;
            }
            if (checkStatus)
            {
                EditDataDialog_UrlCheckIcon.Glyph = "\xE73E";
                EditDataDialog_UrlCheckText.Text = "平台Url合法";
                if (EditDataCheck())
                {
                    IsPrimaryButtonEnabled = true;
                }
                else
                {
                    IsPrimaryButtonEnabled = false;
                }
            }
            else
            {
                EditDataDialog_UrlCheckIcon.Glyph = "\xE711";
                EditDataDialog_UrlCheckText.Text = "请使用http开头的完整Url格式";
                IsPrimaryButtonEnabled = false;
            }
        }
        else
        {
            EditDataDialog_UrlCheckIcon.Glyph = "\xE946";
            EditDataDialog_UrlCheckText.Text = "Url为空，可选择添加链接";
            if (EditDataCheck())
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }
    }

    private void EditDataDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        /*Test info*/
        /* 20230123
        1. NoChange -- 测试通过
        2. ChangeUrl -- 测试未通过，url数据成功更新，但page未刷新，需重新NavigateTo才能实现
        4. ChangeAccountName -- 测试未通过，AccountName成功更新，文本框未更新，侧栏小标题未更新
        5. CHangePlatformName -- 测试通过，原数据删除，增加新数据，自动刷新
        重大bug:修改时两个列表中的GPData时间不一致，导致无法2次修改
        /* 20230125
        在ListDetailsDetailControl.xaml.cs中添加更新数据响应函数
        1. NoChange -- 测试通过
        2. ChangeUrl -- 测试通过，且无二次修改问题
        3. ChangePassword -- 测试通过，且无二次修改问题
        4. ChangeAccountName -- 测试未通过，VM中AccountName被未知进程修改，使VM修改报错
        5. ChangePlatformName -- 测试未通过，修改平台名回到原名时出错
        bug list:
        重大bug:修改时两个列表中的GPData时间不一致，导致无法2次修改(似乎在无断点情况下不会复现)
        新增bug，锁定后再次进入编辑时均报错。
        */
        /*end Test info*/
        if (EditDataDialog_PlatformBox.Text == oldPlatformName && EditDataDialog_AccountBox.Text == oldAccountName && EditDataDialog_PasswordBox.Password == oldPassword && EditDataDialog_PlatformUrlBox.Text == oldPlatformUrl)
        {
            this.Result = EditDataResult.Nochange;
        }
        else
        {
            var accountNameChanged = false;
            if (EditDataDialog_PlatformUrlBox.Text != oldPlatformUrl)
            {
                newPlatformUrl = EditDataDialog_PlatformUrlBox.Text;
                var targetItem = App.DataManager.GetData(oldPlatformName, oldAccountName);
                var check = App.DataManager.ChangeUrl(oldPlatformName, oldAccountName, EditDataDialog_PlatformUrlBox.Text);
                if (check)
                {
                    App.ListDetailsVM.ChangeItemUrl(targetItem, EditDataDialog_PlatformUrlBox.Text);
                    this.Result = EditDataResult.Success;
                }
                else
                {
                    this.Result = EditDataResult.Failure;
                }
            }
            if (EditDataDialog_PasswordBox.Password != oldPassword)
            {
                newPassword = EditDataDialog_PasswordBox.Password;
                var targetItem = App.DataManager.GetData(oldPlatformName, oldAccountName);
                var check = App.DataManager.ChangePassword(oldPlatformName, oldAccountName, EditDataDialog_PasswordBox.Password);
                switch (check)
                {
                    case "Success":
                        App.ListDetailsVM.ChangeItemPassword(targetItem , EditDataDialog_PasswordBox.Password);
                        this.Result = EditDataResult.Success;
                        break;
                    case "SamePassword":
                        this.Result = EditDataResult.Failure;
                        break;
                    case "Empty":
                        this.Result = EditDataResult.Failure;
                        break;
                    case "Unknown Error":
                        this.Result = EditDataResult.UnknowError;
                        break;
                }
            }
            if (EditDataDialog_AccountBox.Text != oldAccountName)
            {
                newAccountName = EditDataDialog_AccountBox.Text;
                var targetItem = App.DataManager.GetData(oldPlatformName, oldAccountName);
                var check = App.DataManager.ChangeAccountName(oldPlatformName, oldAccountName, EditDataDialog_AccountBox.Text);
                if (check)
                {
                    check = App.ListDetailsVM.ChangeItemAccountName(targetItem, EditDataDialog_AccountBox.Text);
                    if (check)
                    {
                        this.Result = EditDataResult.Success;
                        accountNameChanged = true;
                    }
                    else
                        this.Result = EditDataResult.Failure;
                }
                else
                {
                    this.Result = EditDataResult.Failure;
                }
            }
            if (EditDataDialog_PlatformBox.Text != oldPlatformName)
            {
                newPlatformName = EditDataDialog_PlatformBox.Text;
                if (accountNameChanged)
                {
                    var newAccountName = EditDataDialog_AccountBox.Text;
                    var targetItem = App.DataManager.GetData(oldPlatformName, newAccountName);
                    var check = App.DataManager.ChangePlatformName(oldPlatformName, newAccountName, EditDataDialog_PlatformBox.Text);
                    if (check)
                    {
                        check = App.ListDetailsVM.ChangeItemPlatformName(targetItem, EditDataDialog_PlatformBox.Text);
                        if (check)
                            this.Result = EditDataResult.Success;
                        else
                            this.Result = EditDataResult.Failure;
                    }
                    else
                    {
                        this.Result = EditDataResult.Failure;
                    }
                }
                else
                {
                    var targetItem = App.DataManager.GetData(oldPlatformName, oldAccountName);
                    var check = App.DataManager.ChangePlatformName(oldPlatformName, oldAccountName, EditDataDialog_PlatformBox.Text);
                    if (check)
                    {
                        check = App.ListDetailsVM.ChangeItemPlatformName(targetItem, EditDataDialog_PlatformBox.Text);
                        if (check)
                            this.Result = EditDataResult.Success;
                        else
                            this.Result = EditDataResult.Failure;
                    }
                    else
                    {
                        this.Result = EditDataResult.Failure;
                    }
                }
            }
        }
    }

    private bool EditDataCheck()
    {
        if (EditDataDialog_PlatformBox.Text != String.Empty && EditDataDialog_AccountBox.Text != String.Empty && EditDataDialog_PasswordBox.Password != String.Empty)
        {
            if (EditDataDialog_UrlCheckText.Text == "平台Url合法" || EditDataDialog_UrlCheckText.Text == "Url为空，可选择添加链接")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
