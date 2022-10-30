using GoodPass.Services;
using GoodPass.ViewModels;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

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
        var passwordInput = Login_PssswordBox.Password;
        var MKCheck_Result = MKS.CheckMasterKey(passwordInput);
        //添加解锁逻辑
        if (MKCheck_Result == "pass")
        {
            App.App_UnLock();
            ViewModel.Login_UnLock();
        }
        else if (MKCheck_Result == "npass")
        {
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Message = "密码错误，请检查后重试！";//底部横幅提示
        }
        else if (MKCheck_Result == "error: not found")
        {
            //报错：MKConfig路径不存在
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Message = "配置文件不存在！";
            //To Do: 添加进入设置密码界面
        }
        else if (MKCheck_Result == "error: data broken")
        {
            //报错：MKConfig数据损坏
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Message = "配置文件损坏，请修复！";
            //To Do: 添加进入重设密码界面
        }
        else
        {
            //报错：未知错误
            Login_InfoBar.IsOpen = true;
            Login_InfoBar.Message = "未知错误！";
        }
    }

    private async void ShowDialog_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        SetMKDialog dialog = new();

        // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
        dialog.XamlRoot = this.XamlRoot;
        dialog.Style = App.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Content = new SetMKDialogContent();

        var result = await dialog.ShowAsync();
    }

    //private void UnLock()=> NavigationService.NavigateTo(typeof(ListDetailsViewModel).FullName!);
}
