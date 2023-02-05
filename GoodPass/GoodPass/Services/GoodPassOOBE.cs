using GoodPass.Contracts.Services;
using GoodPass.Models;

namespace GoodPass.Services;

/// <summary>
/// Provide GoodPass Out-Of-Box-Experience Services
/// </summary>
public class OOBEServices
{
    private const string oobeSettingKey = "OOBE";

    private readonly ILocalSettingsService _localSettingsService;

    private OOBESituation _OOBESituation
    {
        get; set; 
    }

    public OOBEServices(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;
    }

    public async Task<OOBESituation> GetOOBEStatusAsync()
    {
        var loaclstatus = await _localSettingsService.ReadSettingAsync<string>(oobeSettingKey);
        switch (loaclstatus)
        {
            case "Enable":
                _OOBESituation = OOBESituation.EnableOOBE;
                break;
            case "Disable":
                _OOBESituation = OOBESituation.DIsableOOBE;
                break;
            default:
                _OOBESituation = OOBESituation.EnableOOBE;
                break;
        }
        return _OOBESituation;
    }

    public async Task<bool> SetOOBEStatusAsync(OOBESituation oobeSituation)
    {
        _OOBESituation = oobeSituation;
        switch (oobeSituation)
        {
            case OOBESituation.EnableOOBE:
                await _localSettingsService.SaveSettingAsync(oobeSettingKey, "Enable");
                return true;
            case OOBESituation.DIsableOOBE:
                await _localSettingsService.SaveSettingAsync(oobeSettingKey, "Disable");
                return true;
            default:
                return false;
        }
    }
}