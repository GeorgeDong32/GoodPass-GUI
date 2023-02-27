using Windows.Storage;

namespace GoodPass.Helpers;

public static class SecurityStatusHelper
{
    public static async Task<bool> GetMSPassportStatus()
    {
        if (RuntimeHelper.IsMSIX)
        {
            bool MSPS;
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("MSPassport", out var obj))
            {
                MSPS = (bool)obj;
                await Task.CompletedTask;
                return MSPS;
            }
            else
            {
                MSPS = false;
                return MSPS;
            }
        }
        else
        {
            throw new GPRuntimeException("GetMSPassportStatus: Not in MSIX");
        }
    }

    public static async Task<bool> SetMSPassportStatus(bool value)
    {
        if (RuntimeHelper.IsMSIX)
        {
            ApplicationData.Current.LocalSettings.Values["MSPassport"] = value.ToString();
            await Task.CompletedTask;
            return true;
        }
        else
        {
            return false;
        }
    }
}