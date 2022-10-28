using System.Security.Cryptography;
var teststring = "test";
try
{
    SHA256 SHA256CSP
        = SHA256.Create();

    byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(teststring);
    byte[] bytHash = SHA256CSP.ComputeHash(bytValue);
    SHA256CSP.Clear();

    //根据计算得到的Hash码翻译为SHA-1码
    string sHash = "", sTemp = "";
    for (int counter = 0; counter < bytHash.Count(); counter++)
    {
        long i = bytHash[counter] / 16;
        if (i > 9)
        {
            sTemp = ((char)(i - 10 + 0x41)).ToString();
        }
        else
        {
            sTemp = ((char)(i + 0x30)).ToString();
        }
        i = bytHash[counter] % 16;
        if (i > 9)
        {
            sTemp += ((char)(i - 10 + 0x41)).ToString();
        }
        else
        {
            sTemp += ((char)(i + 0x30)).ToString();
        }
        sHash += sTemp;
    }

    //根据大小写规则决定返回的字符串
    Console.WriteLine(sHash.ToUpper());
}
catch (Exception ex)
{
    throw new Exception(ex.Message);
}