using GoodPass.Models;

namespace GoodPass.Services;

public class GoodPassDataService
{
    private List<GPData> _allDatas;

    private static IEnumerable<GPData> AllDatas()
    {
        var manager = App.DataManager;
        var datas = manager.GetAllDatas();
        if (datas != null)
        {
            return datas;
        }
        else
        {
            datas = new List<GPData>()
            {
                new GPData("Sample", "https://github.com/GeorgeDong32/GoodPass", "SampleAccount", App.PublicGPCS.DecryptStr("SamplePassword"), DateTime.Now)
            };
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
