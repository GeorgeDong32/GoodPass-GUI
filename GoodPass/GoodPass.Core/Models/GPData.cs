namespace GoodPass.Core.Models;
public class GPData
{
    /*数据*/
    public string PlatformName
    {
        get; set;
    }

    public string PlatformUrl
    {
        get; set;
    }

    public string AccountName
    {
        get; set;
    }

    private string EncPassword
    {
        get; set;
    }

    public string DecPassword
    {
        get; set;
    }

    public DateTime LatestUpdateTime
    {
        get; set;
    }

    /*方法*/
    public void selfUpdate()
    {
        /*Todo:添加数据自升级的相应代码*/
        LatestUpdateTime = DateTime.Now;
    }

    public void dataDecrypt()
    {
        /*Todo:添加使用GPSESservice解密的实现*/
        DecPassword = EncPassword;
    }

    public string getPassword() => DecPassword;
}
