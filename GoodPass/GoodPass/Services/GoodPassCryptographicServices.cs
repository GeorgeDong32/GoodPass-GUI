namespace GoodPass.Services;

/// <summary>
/// 提供GPSES服务，用于对数据进行加解密
/// </summary>
public class GoodPassCryptographicServices
{
    /// <summary>
    /// 加密基数组
    /// </summary>
    private int[]? CryptBase
    {
        get; set;
    }

    /// <summary>
    /// 数字字符index数组
    /// </summary>
    private int[] NumPos
    {
        get; set;
    }

    /// <summary>
    /// 特殊字符index数组
    /// </summary>
    private int[] SpecPos
    {
        get; set;
    }

    /// <summary>
    /// GoodPassCryptographicServices构造函数
    /// </summary>
    public GoodPassCryptographicServices()
    {
        CryptBase = App.MKBase;
        NumPos = new int[41];
        SpecPos = new int[41];
        NumPos[0] = 0; SpecPos[0] = 0;
    }

    /// <summary>
    /// (测试用)GoodPassCryptographicServices构造函数
    /// </summary>
    /// <param name="cryptBase">加密基</param>
    public GoodPassCryptographicServices(int[] cryptBase)
    {
        CryptBase = cryptBase;
        NumPos = new int[41];
        SpecPos = new int[41];
        NumPos[0] = 0; SpecPos[0] = 0;
    }



    /// <summary>
    /// 解密输入的字符串
    /// </summary>
    /// <param name="input">待解密的字符串</param>
    /// <returns>解密后字符串</returns>
    /// <exception cref="ArgumentNullException">空输入异常</exception>
    public string DecryptStr(string input)
    {
        //确保CryptBase已经赋值
        CryptBase = App.MKBase;
        if (input == null || input == string.Empty)
        {
            throw new ArgumentNullException("DecryptStr: input is null or empty");
        }
        var decStr = "";
        //找数字位置
        NumPos[0] = (int)input[0] - 'A';
        for (var i = 1; i <= NumPos[0]; i++)
        {
            switch (i % 2)
            {
                case 0:
                    NumPos[i] = (int)input[i] - 97;
                    break;
                case 1:
                    NumPos[i] = (int)input[i] - 65;
                    break;
            }
        }
        //找特殊字符位置
        var retemp = input.ToCharArray();
        Array.Reverse(retemp);
        var reinput = new string(retemp);
        SpecPos[0] = (int)reinput[0] - 'A';
        for (var i = 1; i <= SpecPos[0]; i++)
        {
            SpecPos[i] = (int)reinput[i] - 65;
        }
        for (var i = NumPos[0] + 1; i < input.Length - SpecPos[0] - 1; i++)
        {
            var actLength = i - NumPos[0] - 1;
            if (Array.FindIndex(NumPos, 1, p => p == i) != -1)//判断是否为数字
            {
                switch (actLength % 2)
                {
                    case 0:
                        decStr += (char)(input[i] - 'e' - CryptBase[actLength] + '0');
                        break;
                    case 1:
                        decStr += (char)(input[i] - 'O' - CryptBase[actLength] + '0');
                        break;
                }
            }
            else if (Array.FindIndex(SpecPos, 1, p => p == i) != -1)
            {
                decStr += input[i];
            }
            else
            {
                if (input[i] >= 58 && input[i] <= 92)
                {
                    decStr += (char)(input[i] + 39 - CryptBase[actLength]);
                }
                else
                {
                    decStr += (char)(input[i] - 28 - CryptBase[actLength]);
                }
            }
        }
        return decStr;
    }

    /// <summary>
    /// (测试用)解密字符串
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public string TDecryptStr(string input)
    {
        if (input == null || input == string.Empty)
        {
            throw new ArgumentNullException("DecryptStr: input is null or empty");
        }
        var decStr = "";
        //找数字位置
        NumPos[0] = (int)input[0] - 'A';
        for (var i = 1; i <= NumPos[0]; i++)
        {
            switch (i % 2)
            {
                case 0:
                    NumPos[i] = (int)input[i] - 97;
                    break;
                case 1:
                    NumPos[i] = (int)input[i] - 65;
                    break;
            }
        }
        //找特殊字符位置
        var retemp = input.ToCharArray();
        Array.Reverse(retemp);
        var reinput = new string(retemp);
        SpecPos[0] = (int)reinput[0] - 'A';
        for (var i = 1; i <= SpecPos[0]; i++)
        {
            SpecPos[i] = (int)reinput[i] - 65;
        }
        for (var i = NumPos[0] + 1; i < input.Length - SpecPos[0] - 1; i++)
        {
            var actLength = i - NumPos[0] - 1;
            if (Array.FindIndex(NumPos, 1, p => p == i) != -1)//判断是否为数字
            {
                switch (actLength % 2)
                {
                    case 0:
                        decStr += (char)(input[i] - 'e' - CryptBase[actLength] + '0');
                        break;
                    case 1:
                        decStr += (char)(input[i] - 'O' - CryptBase[actLength] + '0');
                        break;
                }
            }
            else if (Array.FindIndex(SpecPos, 1, p => p == i) != -1)
            {
                decStr += input[i];
            }
            else
            {
                if (input[i] >= 58 && input[i] <= 92)
                {
                    decStr += (char)(input[i] + 39 - CryptBase[actLength]);
                }
                else
                {
                    decStr += (char)(input[i] - 28 - CryptBase[actLength]);
                }
            }
        }
        return decStr;
    }

    /// <summary>
    /// 加密输入的字符串
    /// </summary>
    /// <param name="input">待加密字符串</param>
    /// <returns>加密后字符串</returns>
    public string EncryptStr(string input)//Todo：测试char-int是否按要求转换
    {
        //确保CryptBase已经赋值
        CryptBase = App.MKBase;
        //找数字位置
        var Strlength = input.Length;
        var npCount = 1;
        var specCount = 1;
        var output = "";
        for (var i = 0; i < Strlength; i++)
        {
            if ((int)input[i] >= 48 && (int)input[i] <= 57)
            {
                NumPos[npCount] = i;
                npCount++;
                NumPos[0]++;
            }
        }
        //全串加密
        for (var i = 0; i < Strlength; i++)//未考虑特殊字符加密
        {
            var temp = (int)input[i];
            if (temp >= 48 && temp <= 57)
            {
                if (i % 2 == 0)
                {
                    temp = 'e' + temp - '0' + CryptBase[i];
                }
                else
                {
                    temp = 'O' + temp - '0' + CryptBase[i];
                }

                output += (char)temp;
            }
            else if (temp >= 65 && temp <= 90)
            {
                temp += 28;
                output += (char)(temp + CryptBase[i]);
            }
            else if (temp >= 97 && temp <= 122)
            {
                temp -= 39;
                output += (char)(temp + CryptBase[i]);
            }
            else
            {
                output += (char)temp;
                SpecPos[specCount] = i;
                specCount++;
            }
        }
        //生成指示串头
        var head = "";
        head += (char)(NumPos[0] + 'A');
        for (var i = 1; i <= NumPos[0]; i++)
        {
            switch (i % 2)
            {
                case 0:
                    head += (char)(NumPos[i] + 97);
                    break;
                case 1:
                    head += (char)(NumPos[i] + 65);
                    break;
            }
        }
        //生成指示串尾
        var tail = "";
        for (var i = 1; i < SpecPos[0]; i++)
        {
            tail += (char)(SpecPos[i] + 65);
        }
        tail += (char)(SpecPos[0] + 65);
        output = head + output + tail;
        return output;
    }

    /// <summary>
    /// (测试用)加密输入的字符串
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public string TEncryptStr(string input)
    {
        //找数字位置
        var Strlength = input.Length;
        var npCount = 1;
        var specCount = 1;
        var output = "";
        for (var i = 0; i < Strlength; i++)
        {
            if ((int)input[i] >= 48 && (int)input[i] <= 57)
            {
                NumPos[npCount] = i;
                npCount++;
                NumPos[0]++;
            }
        }
        //全串加密
        for (var i = 0; i < Strlength; i++)//未考虑特殊字符加密
        {
            var temp = (int)input[i];
            if (temp >= 48 && temp <= 57)
            {
                if (i % 2 == 0)
                {
                    temp = 'e' + temp - '0' + CryptBase[i];
                }
                else
                {
                    temp = 'O' + temp - '0' + CryptBase[i];
                }

                output += (char)temp;
            }
            else if (temp >= 65 && temp <= 90)
            {
                temp += 28;
                output += (char)(temp + CryptBase[i]);
            }
            else if (temp >= 97 && temp <= 122)
            {
                temp -= 39;
                output += (char)(temp + CryptBase[i]);
            }
            else
            {
                output += (char)temp;
                SpecPos[specCount] = i;
                specCount++;
            }
        }
        //生成指示串头
        var head = "";
        head += (char)(NumPos[0] + 'A');
        for (var i = 1; i <= NumPos[0]; i++)
        {
            switch (i % 2)
            {
                case 0:
                    head += (char)(NumPos[i] + 97);
                    break;
                case 1:
                    head += (char)(NumPos[i] + 65);
                    break;
            }
        }
        //生成指示串尾
        var tail = "";
        for (var i = 1; i < SpecPos[0]; i++)
        {
            tail += (char)(SpecPos[i] + 65);
        }
        tail += (char)(SpecPos[0] + 65);
        output = head + output + tail;
        return output;
    }
}