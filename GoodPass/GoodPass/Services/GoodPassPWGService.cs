namespace GoodPass.Services;

/// <summary>
/// GoodPass密码生成服务
/// </summary>
public static class GoodPassPWGService
{
    #region Password Generate Functions
    /// <summary>
    /// 生成不含特殊字符的随机密码
    /// </summary>
    /// <param name="length">生成密码长度</param>
    public static string RandomPasswordNormal(int length)
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

    /// <summary>
    /// 生成含特殊字符的随机密码
    /// </summary>
    public static string RandomPasswordSpec(int length)
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

    /// <summary>
    /// 生成GoodPass风格密码
    /// </summary>
    public static string GPstylePassword(string platformName, string accountName)
    {
        var random = new Random();
        #region 平台名处理
        var PNLength = platformName.Length;
        int temp; char upcaseTemp;
        var platn = platformName;
        if (PNLength <= 5)
        {
            temp = random.Next(0, PNLength);
            //将platn上temp位置的字母变为大写
            upcaseTemp = platn[temp];
            if ((int)upcaseTemp >= 97)
            {
                upcaseTemp = (char)(upcaseTemp - 32);
            }
            platn = platn.Remove(temp, 1);
            platn = platn.Insert(temp, upcaseTemp.ToString());
        }
        else
        {
            for (var i = 0; i < 2; i++)
            {
                temp = random.Next(0, PNLength);
                //将platn上temp位置的字母变为大写
                upcaseTemp = platn[temp];
                if ((int)upcaseTemp >= 97)
                {
                    upcaseTemp = (char)(upcaseTemp - 32);
                }
                platn = platn.Remove(temp, 1);
                platn = platn.Insert(temp, upcaseTemp.ToString());
            }
        }
        #endregion

        #region 账号名处理
        string accn;
        if (accountName.StartsWith("@"))
        {
            accn = ProcessUsername(accountName[1..]);
        }
        else
        {
            accn = "@";
            accn += ProcessUsername(accountName);
        }
        #endregion

        #region Time Patch
        var time = DateTime.Now;
        var timePatch1 = (char)(48 + time.Month + time.Minute);
        var timePatch2 = (char)(48 + time.Hour + time.Day);
        var choosetime = random.Next(1, 5);
        var timePatch3 = choosetime switch
        {
            1 => time.Month,
            2 => time.Hour,
            3 => time.Minute,
            4 => time.Second,
            _ => 32,
        };
        var timePatch = timePatch3.ToString() + timePatch1.ToString() + timePatch2.ToString();
        #endregion

        //整合
        var gpPassword = platn + accn + timePatch;
        return gpPassword;
    }

    public static string ProcessUsername(string username)
    {
        if (username == string.Empty || username is null)
        {
            throw new ArgumentNullException("ProcessUsername: username is null or empty");
        }
        else
        {
            string output;
            if (username.Contains('@'))
            {
                output = username[0..3];
                var domainname = username.Substring((username.IndexOf('@') + 1), Math.Min(10, username.Length - username.IndexOf('@') - 1));
                if (domainname.StartsWith("outlook"))
                {
                    output += "out";
                }
                else if (domainname.StartsWith("gmail"))
                {
                    output += "gm";
                }
                else if (domainname.StartsWith("qq"))
                {
                    output += "qq";
                }
                else if (domainname.StartsWith("foxmail"))
                {
                    output += "fm";
                }
                else
                {
                    output += domainname[0..2];
                }
                return output;
            }
            else
            {
                output = username[0..5];
                return output;
            }
        }
    }
    #endregion
}