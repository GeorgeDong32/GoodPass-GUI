namespace GoodPass.Services;

//提供GPSES服务，用于对数据进行加解密
public class GoodPassCryptographicServices
{
    /*成员*/
    private int[]? CryptBase
    {
        get; set;
    }

    /*方法*/
    public GoodPassCryptographicServices()
    {
        CryptBase = App.MKBase;
    }

    public string decryptStr(string input)
    {
        //添加解密代码
        return input;
    }

    public string encryptStr(string input)
    {
        //添加加密代码
        return input;
    }
}