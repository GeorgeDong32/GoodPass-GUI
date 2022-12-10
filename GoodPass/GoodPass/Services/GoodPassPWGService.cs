namespace GoodPass.Services;

public class GoodPassPWGService //密码生成服务(随机密码+指定格式密码)
{
    public string randomPasswordNormal(int length) //生成随机密码
    {
        var random = new Random();
        var password = "";
        for (var i = 0; i < length; i++)
        {
            var temp = random.Next(0, 3);
            switch (temp)
            {
                case 0:
                    password += (char)random.Next(48, 58);
                    break;
                case 1:
                    password += (char)random.Next(65, 91);
                    break;
                case 2:
                    password += (char)random.Next(97, 123);
                    break;
            }
        }
        return password;
    }

    public string randomPasswordSpec(int length) //生成含特殊字符的随机密码
    {
        var random = new Random();
        var password = "";
        for (var i = 0; i < length; i++)
        {
            var temp = random.Next(0, 4);
            switch (temp)
            {
                case 0:
                    password += (char)random.Next(48, 58);
                    break;
                case 1:
                    password += (char)random.Next(65, 91);
                    break;
                case 2:
                    password += (char)random.Next(97, 123);
                    break;
                case 3:
                    password += (char)random.Next(33, 48);
                    break;
            }
        }
        return password;
    }

    public string gpstylePassword(string platformName, string accountName) //生成GoodPass风格密码
    {
        var gpPassword = "";
        var random = new Random();
        //对平台名进行大小写处理
        var PNLength = platformName.Length;
        var temp = 0; char upcaseTemp;
        var platn = platformName;
        if (PNLength <= 5)
        {
            temp = random.Next(0, PNLength);
            //将gpPassword上temp位置的字母变为大写
            upcaseTemp = gpPassword[temp];
            if ((int)upcaseTemp >= 97)
            {
                upcaseTemp = (char)(upcaseTemp - 32);
            }
            gpPassword = gpPassword.Remove(temp, 1);
            gpPassword = gpPassword.Insert(temp, upcaseTemp.ToString());
        }
        else
        {
            for (var i = 0; i < 2; i++)
            {
                temp = random.Next(0, PNLength);
                //将gpPassword上temp位置的字母变为大写
                upcaseTemp = gpPassword[temp];
                if ((int)upcaseTemp >= 97)
                {
                    upcaseTemp = (char)(upcaseTemp - 32);
                }
                gpPassword = gpPassword.Remove(temp, 1);
                gpPassword = gpPassword.Insert(temp, upcaseTemp.ToString());
            }
        }
        //处理账号名
        var accn = "";
        if (accountName.StartsWith("@"))
        {
            accn = accountName.Substring(0, 4);
        }
        else
        {
            accn = "@";
            accn += accountName.Substring(0, 3);
        }
        //处理时间戳补强串
        var timePatch = "";
        var time = DateTime.Now;
        char timePatch1 = (char)(64 + time.Month + time.Day);
        char timePatch2 = (char)(64 + time.Hour + time.Minute);
        timePatch = timePatch1.ToString() + timePatch2.ToString();
        temp = random.Next(0, 2);
        switch (temp)
        {
            case 0:
                timePatch.ToLower();
                break;
            case 1:
                timePatch.ToUpper();
                break;
        }
        //整合
        gpPassword = platn + accn + timePatch;
        return gpPassword;
    }
}
