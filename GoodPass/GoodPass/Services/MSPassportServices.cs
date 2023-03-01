using System.Diagnostics;
using Windows.Security.Credentials;

namespace GoodPass.Services;

public static class MicrosoftPassportHelper
{
    /// <summary>
    /// Checks to see if Passport is ready to be used.
    /// 
    /// Passport has dependencies on:
    ///     1. Having a connected Microsoft Account
    ///     2. Having a Windows PIN set up for that _account on the local machine
    /// </summary>
    public static async Task<bool> MicrosoftPassportAvailableCheckAsync()
    {
        bool keyCredentialAvailable = await KeyCredentialManager.IsSupportedAsync();
        if (keyCredentialAvailable == false)
        {
            // Key credential is not enabled yet as user 
            // needs to connect to a Microsoft Account and select a PIN in the connecting flow.
            Debug.WriteLine("Microsoft Passport is not setup!\nPlease go to Windows Settings and set up a PIN to use it.");
            return false;
        }

        return true;
    }
}