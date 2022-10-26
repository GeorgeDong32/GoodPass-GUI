using GoodPass.Services;
using GoodPass.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace GoodPass.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MasterKeyService MKS
    { 
        get;  
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        MKS = App.GetService<MasterKeyService>();
        InitializeComponent();
    }

    private void Login_Check_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        string passwordInput = Login_PssswordBox.Password;
        var MKCheck_Result = MKS.CheckMasterKey(passwordInput);
        //添加解锁逻辑
        if (MKCheck_Result == "pass")
        {
            App.App_UnLock();
            ViewModel.Login_UnLock();
        }
        else if (MKCheck_Result == "npass")
        {
            //显示密码错误弹窗或提示
        }
        else if (MKCheck_Result == "error: not found")
        {
            //报错：MKConfig路径不存在
        }
        else if (MKCheck_Result == "error: data broken")
        {
            //报错：MKConfig数据损坏
        }
        else
        {
            //报错：未知错误
        }
    }

    //private void UnLock()=> NavigationService.NavigateTo(typeof(ListDetailsViewModel).FullName!);
}
