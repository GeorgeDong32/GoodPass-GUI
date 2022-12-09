using GoodPass.Services;

namespace GoodPass.Models;

public class GPData
{
    /*数据*/
    public string PlatformName
    {
        get; set;
    }

    public string? PlatformUrl
    {
        get; set;
    }

    public string AccountName
    {
        get; set;
    }

    public string EncPassword
    {
        get; set;
    }

    private string DecPassword
    {
        get; set;
    }

    public DateTime LatestUpdateTime
    {
        get; set;
    }

    /*方法*/
    public GPData()
    {
        PlatformName = "No Name";
        PlatformUrl = null;
        AccountName = "No Name";
        DecPassword = "DecPassword";
        EncPassword = DecPassword;
        LatestUpdateTime = DateTime.Now;
    }

    public GPData(string platformName, string platformUrl, string accountName, string encPassword, DateTime latestUpdateTime)
    {
        PlatformName = platformName;
        PlatformUrl = platformUrl;
        AccountName = accountName;
        EncPassword = encPassword;
        DecPassword = EncPassword;
        LatestUpdateTime = latestUpdateTime;
    }

    public void selfUpdate() //预留接口
    {
        /*Todo:添加数据自升级的相应代码*/
        LatestUpdateTime = DateTime.Now;
    }

    public bool dataDecrypt()
    {
        var GPCS = new GoodPassCryptographicServices();
        DecPassword = GPCS.decryptStr(EncPassword);
        return true;
    }

    public string getPassword() => DecPassword;

    public string changePassword(string newPassword)
    {
        dataDecrypt();
        if (newPassword != DecPassword && newPassword != String.Empty && newPassword != null)
        {
            var GPCS = new GoodPassCryptographicServices();
            DecPassword = newPassword;
            EncPassword = GPCS.encryptStr(newPassword);
            LatestUpdateTime = DateTime.Now;
            return "Success";
        }
        else if (newPassword == DecPassword)
        {
            return "SamePassword";
        }
        else if (newPassword == String.Empty || newPassword == null)
        {
            return "Empty";
        }
        else
        {
            return "Unknown Error";
        }
    }
}
