using Windows.Storage;

namespace GoodPass.Helpers;

public static class SecurityStatusHelper
{
    public static async Task<bool> GetMSPassportStatusAsync()
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

    public static async Task<bool> SetMSPassportStatusAsync(bool value)
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

    public static async Task<bool> GetDataInsetStatusAsync()
    {
        if (RuntimeHelper.IsMSIX)
        {
            bool DIS;
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("DataInsert", out var obj))
            {
                DIS = (bool)obj;
                await Task.CompletedTask;
                return DIS;
            }
            else
            {
                DIS = false;
                return DIS;
            }
        }
        else
        {
            throw new GPRuntimeException("GetDataInsetStatusAsync: Not in MSIX");
        }
    }

    public static async Task<bool> SetDataInsetStatusAsync(bool value)
    {
        if (RuntimeHelper.IsMSIX)
        {
            ApplicationData.Current.LocalSettings.Values["DataInsert"] = value.ToString();
            await Task.CompletedTask;
            return true;
        }
        else
        {
            return false;
        }
    }

    public static async Task<bool> GetAESStatusAsync()
    {
        if (RuntimeHelper.IsMSIX)
        {
            bool AESS;
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue("AESStatus", out var obj))
            {
                AESS = (bool)obj;
                await Task.CompletedTask;
                return AESS;
            }
            else
            {
                AESS = false;
                return AESS;
            }
        }
        else
        {
            throw new GPRuntimeException("GetAESStatusAsync: Not in MSIX");
        }
    }

    public static async Task<bool> SetAESStatusAsync(bool value)
    {
        if (RuntimeHelper.IsMSIX)
        {
            ApplicationData.Current.LocalSettings.Values["AESStatus"] = value.ToString();
            await Task.CompletedTask;
            return true;
        }
        else
        {
            return false;
        }
    }
}