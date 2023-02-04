namespace GoodPass.Services;

/// <summary>
/// Provide GoodPass Out-Of-Box-Experience Services
/// </summary>
public class OOBEServices
{
    private const string oobeSettingKey = "OOBE";

    private readonly LocalSettingsService _localSettingsService;

    public OOBEServices()
    {
        _localSettingsService = App.GetService<LocalSettingsService>();
    }

    
}