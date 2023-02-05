namespace GoodPass.Strings;

/// <summary>
/// UI控件字符串类，为无法使用resw的控件提供多语言支持
/// </summary>
public class UIStrings
{
    //TODO: 添加控件字符串位置
    public readonly string ListDetailsDetailControl_AccountNameTitleText;

    public readonly string ListDetailsDetailControl_CopiedTipforAcconutNameCopyButtonTitle;

    public readonly string ListDetailsDetailControl_PasswordTitleText;

    public readonly string ListDetailsDetailControl_CopiedTipforPasswordCopyButtonTitle;

    public readonly string ListDetailsDetailControl_PlatformUrlTitleText;

    public readonly string ListDetailsDetailControl_LastmodifiedTitleText;

    public UIStrings(string language)
    {
        switch (language)
        {
            case "zh-CN":
                ListDetailsDetailControl_AccountNameTitleText = "账号";
                ListDetailsDetailControl_CopiedTipforAcconutNameCopyButtonTitle = "账号名已复制！";
                ListDetailsDetailControl_PasswordTitleText = "密码";
                ListDetailsDetailControl_CopiedTipforPasswordCopyButtonTitle = "密码已复制！";
                ListDetailsDetailControl_PlatformUrlTitleText = "平台网址";
                ListDetailsDetailControl_LastmodifiedTitleText = "最后修改时间";
                break;
            case "en-US":
                ListDetailsDetailControl_AccountNameTitleText = "Account";
                ListDetailsDetailControl_CopiedTipforAcconutNameCopyButtonTitle = "AccountName has copied!";
                ListDetailsDetailControl_PasswordTitleText = "Password";
                ListDetailsDetailControl_CopiedTipforPasswordCopyButtonTitle = "Password has copied!";
                ListDetailsDetailControl_PlatformUrlTitleText = "Platform Url";
                ListDetailsDetailControl_LastmodifiedTitleText = "Last modified";
                break;
            default:
                ListDetailsDetailControl_AccountNameTitleText = "账号";
                ListDetailsDetailControl_CopiedTipforAcconutNameCopyButtonTitle = "已复制";
                ListDetailsDetailControl_PasswordTitleText = "密码";
                ListDetailsDetailControl_CopiedTipforPasswordCopyButtonTitle = "已复制";
                ListDetailsDetailControl_PlatformUrlTitleText = "平台网址";
                ListDetailsDetailControl_LastmodifiedTitleText = "最后修改时间";
                break;
        }
    }
}