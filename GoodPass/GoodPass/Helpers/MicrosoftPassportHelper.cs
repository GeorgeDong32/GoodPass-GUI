using GoodPass.Models;
using Windows.Security.Credentials;
using Windows.Security.Credentials.UI;

namespace GoodPass.Helpers;
public static class MicrosoftPassportHelper
{
    #region Available Check Method
    /// <summary>
    /// 检查MSP可用性(在WinUI3中不可用)
    /// </summary>
    public static async Task<bool> MicrosoftPassportAvailableCheckAsync()
    {
        var keyCredentialAvailable = await KeyCredentialManager.IsSupportedAsync();
        if (keyCredentialAvailable == false)
        {
            return false;
        }

        return true;
    }
    #endregion

    #region Create/Remove MSP Key Methods
    /// <summary>
    /// Creates a Passport key on the machine using the _account id passed.
    /// </summary>
    /// <param name="accountId">The _account id associated with the _account that we are enrolling into Passport</param>
    /// <returns>Boolean representing if creating the Passport key succeeded</returns>
    public static async Task<bool> CreatePassportKeyAsync(string accountId)
    {
        var keyCreationResult = await KeyCredentialManager.RequestCreateAsync(accountId, KeyCredentialCreationOption.ReplaceExisting);

        switch (keyCreationResult.Status)
        {
            case KeyCredentialStatus.Success:
                return true;
            case KeyCredentialStatus.UserCanceled:
                break;
            case KeyCredentialStatus.NotFound:
                break;
            default:
                break;
        }
        return false;
    }

    public static async Task<bool> RemovePassportKeyAsync(string accountId)
    {
        await KeyCredentialManager.DeleteAsync(accountId);

        return true;
    }
    #endregion

    #region Sign/Verify Methods
    public static async Task<PassportSignInResult> PassportSignInAsync()
    {
        //TODO:多语言
        var result = await UserConsentVerifier.RequestVerificationAsync(App.UIStrings.MSPLoginTitle);
        return result switch
        {
            UserConsentVerificationResult.Verified => PassportSignInResult.Verified,
            UserConsentVerificationResult.DeviceBusy => PassportSignInResult.Busy,
            UserConsentVerificationResult.DeviceNotPresent => PassportSignInResult.NotUseable,
            UserConsentVerificationResult.DisabledByPolicy => PassportSignInResult.Disabled,
            UserConsentVerificationResult.RetriesExhausted => PassportSignInResult.Failed,
            UserConsentVerificationResult.Canceled => PassportSignInResult.Cancel,
            UserConsentVerificationResult.NotConfiguredForUser => PassportSignInResult.NotUseable,
            _ => PassportSignInResult.NotUseable,
        };
    }
    #endregion
}