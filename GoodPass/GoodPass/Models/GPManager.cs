namespace GoodPass.Models;

public class GPManager
{
    /*成员区*/
    private List<GPData> GPDatas;

    /*方法区*/
    GPManager() => GPDatas = new List<GPData>();

    int[] fuzzySearch(string platformName) //预留接口（模糊搜索）
    {
        var indexArray = new int[1];
        var indexArrayCount = 0;
        foreach (var data in GPDatas)
        {
            if (data.PlatformName == platformName)
            {
                indexArray[indexArrayCount] = GPDatas.IndexOf(data);
                indexArrayCount++;
                Array.Resize(ref indexArray, indexArray.Length + 1);
            }
        }
        return indexArray;
    }

    int accurateSearch(string platformName, string accountName) //预留接口（精确搜索）
    {
        foreach (var data in GPDatas)
        {
            if (data.PlatformName == platformName && data.AccountName == accountName)
            {
                return GPDatas.IndexOf(data);
            }
        }
        return -1;
    }

    bool addData(string platformName, string platformUrl, string accountName, string encPassword)//手动添加数据
    {
        var indexArray = fuzzySearch(platformName);
        foreach (var index in indexArray)
        {
            if (GPDatas[index].AccountName == accountName)
            {
                return false;
            }
        }
        var datatemp = new GPData(platformName, platformUrl, accountName, encPassword, DateTime.Now);
        GPDatas.Add(datatemp);
        return true;
    }

    bool addData(string platformName, string platformUrl, string accountName, string encPassword, DateTime latestUpdateTime)/*自动添加数据*/
    {
        var indexArray = fuzzySearch(platformName);
        foreach (var index in indexArray)
        {
            if (GPDatas[index].AccountName == accountName)
            {
                return false;
            }
        }
        var datatemp = new GPData(platformName, platformUrl, accountName, encPassword, latestUpdateTime);
        GPDatas.Add(datatemp);
        return true;
    }

    bool deleteData(string platformName, string accountName)/*删除数据*/
    {
        var indexArray = fuzzySearch(platformName);
        foreach (var index in indexArray)
        {
            if (GPDatas[index].AccountName == accountName)
            {
                GPDatas.RemoveAt(index);
                return true;
            }
        }
        return false;
    }

    string changeData(string platformName, string accountName, string newPassword)//重新设置密码
    {
        var targetIndex = accurateSearch(platformName, accountName);
        return GPDatas[targetIndex].changePassword(newPassword);
    }

    bool loadFormFile(string filePath)//从文件导入数据
    {
        if (File.Exists(filePath))
        {
            var dataLines = File.ReadLines(filePath);
            dataLines = dataLines.Skip(1); //跳过文件头
            foreach (var line in dataLines)
            {
                var data = line.Split(',');
                addData(data[0], data[1], data[2], data[3], DateTime.Parse(data[4]));
            }
            return true;
        }
        else
        {
            File.Create(filePath);
            return false;
        }
    }

    bool saveToFile(string filePath)//保存数据到文件
    {
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, "PlatformName,PlatformUrl,AccountName,EncPassword,LatestUpdateTime");
            foreach (var data in GPDatas)
            {
                File.AppendAllText(filePath, $"{data.PlatformName},{data.PlatformUrl},{data.AccountName},{data.EncPassword},{data.LatestUpdateTime.ToString()}\n", System.Text.Encoding.UTF8);
            }
            return true;
        }
        else
        {
            File.Create(filePath);
            File.WriteAllText(filePath, "PlatformName,PlatformUrl,AccountName,EncPassword,LatestUpdateTime");
            foreach (var data in GPDatas)
            {
                File.AppendAllText(filePath, $"{data.PlatformName},{data.PlatformUrl},{data.AccountName},{data.EncPassword},{data.LatestUpdateTime.ToString()}\n", System.Text.Encoding.UTF8);
            }
            return true;
        }
    }
}
