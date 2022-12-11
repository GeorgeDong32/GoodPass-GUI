using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using GoodPass.Contracts.ViewModels;
using GoodPass.Models;
using GoodPass.Services;

namespace GoodPass.ViewModels;

public class ListDetailsViewModel : ObservableRecipient, INavigationAware
{
    /*原始代码
    private readonly ISampleDataService _sampleDataService;
    private SampleOrder? _selected;


    public SampleOrder? Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new ObservableCollection<SampleOrder>();

    public ListDetailsViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        SampleItems.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetListDetailsDataAsync();

        foreach (var item in data)
        {
            SampleItems.Add(item);
        }
    }  

    public void EnsureItemSelected()
    {
        if (Selected == null)
        {
            Selected = SampleItems.First();
        }
    }
    /*End 原始代码*/

    public void OnNavigatedFrom()
    {
    }

    /*待部署代码*/
    private readonly GoodPassDataService _dataService;
    private GPData? _selectedData;
    public ObservableCollection<GPData> DataItems { get; private set; } = new ObservableCollection<GPData>();

    public ListDetailsViewModel(GoodPassDataService goodPassDataService)
    {
        _dataService = goodPassDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        DataItems.Clear();

        var datas = await _dataService.GetListDetailsDataAsync();

        foreach (var data in datas)
        {
            DataItems.Add(data);
        }
    }

    public GPData? SlectedData
    {
        get => _selectedData;
        set => SetProperty(ref _selectedData, value);
    }

    public void EnsureItemSelected()
    {
        if (SlectedData == null)
        {
            SlectedData = DataItems.First();
        }
    }
    /*End 待部署代码*/
}
