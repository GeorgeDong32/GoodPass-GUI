using GoodPass.Models;

namespace GoodPass.Services;

public class GoodPassDataService
{
    private List<GPData> _allDatas;

    private static IEnumerable<GPData> AllDatas()
    {
        var manager = App.DataManager;
        manager.DecryptAllDatas();
        var datas = manager.GetAllDatas();
        if (datas != null && datas.Count() != 0)
        {
            return datas;
        }
        else
        {
            datas = new List<GPData>()
            {
                new GPData("Sample", "https://github.com/GeorgeDong32/GoodPass", "SampleAccount", App.PublicGPCS.EncryptStr("SamplePassword"), DateTime.Now)
            };
            datas.First().DataDecrypt();
            return datas;
        }
    }

    public async Task<IEnumerable<GPData>> GetListDetailsDataAsync()
    {
        if (_allDatas == null)
        {
            _allDatas = new List<GPData>(AllDatas());
        }

        await Task.CompletedTask;
        return _allDatas;
    }
}
