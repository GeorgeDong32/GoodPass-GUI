using System.Diagnostics;
using GoodPass.Models;
using Windows.Security.Credentials;
using Windows.Security.Credentials.UI;

namespace GoodPass.Helpers;
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
        var keyCredentialAvailable = await KeyCredentialManager.IsSupportedAsync();
        if (keyCredentialAvailable == false)
        {
            // Key credential is not enabled yet as user 
            // needs to connect to a Microsoft Account and select a PIN in the connecting flow.
            Debug.WriteLine("Microsoft Passport is not setup!\nPlease go to Windows Settings and set up a PIN to use it.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Creates a Passport key on the machine using the _account id passed.
    /// </summary>
    /// <param name="accountId">The _account id associated with the _account that we are enrolling into Passport</param>
    /// <returns>Boolean representing if creating the Passport key succeeded</returns>
    public static async Task<bool> CreatePassportKeyAsync(string accountId)
    {
        KeyCredentialRetrievalResult keyCreationResult = await KeyCredentialManager.RequestCreateAsync(accountId, KeyCredentialCreationOption.ReplaceExisting);

        switch (keyCreationResult.Status)
        {
            case KeyCredentialStatus.Success:
                Debug.WriteLine("Successfully made key");

                //存储密码
                /*var vault = new Windows.Security.Credentials.PasswordVault();
                vault.Add(new Windows.Security.Credentials.PasswordCredential(
                    "GoodPass", "sampleUsername", "TestPasswords"));**/

                //删除密码
                /*var credential = new Windows.Security.Credentials.PasswordVault().Retrieve("GoodPass", "sampleUsername");
                var mid = new Windows.Security.Credentials.PasswordVault();
                mid.Remove(credential);
                Debug.WriteLine("User name: " + credential.Password);*/

                // In the real world authentication would take place on a server.
                // So every time a user migrates or creates a new Microsoft Passport account Passport details should be pushed to the server.
                // The details that would be pushed to the server include:
                // The public key, keyAttesation if available, 
                // certificate chain for attestation endorsement key if available,  
                // status code of key attestation result: keyAttestationIncluded or 
                // keyAttestationCanBeRetrievedLater and keyAttestationRetryType
                // As this sample has no concept of a server it will be skipped for now
                // for information on how to do this refer to the second Passport sample

                //For this sample just return true
                return true;
            case KeyCredentialStatus.UserCanceled:
                Debug.WriteLine("User cancelled sign-in process.");
                break;
            case KeyCredentialStatus.NotFound:
                // User needs to setup Microsoft Passport
                Debug.WriteLine("Microsoft Passport is not setup!\nPlease go to Windows Settings and set up a PIN to use it.");
                break;
            default:
                break;
        }
        return false;
    }

    public static async Task<PassportSignInResult> PassportSignInAsync()
    {
        //TODO:多语言
        var result = await UserConsentVerifier.RequestVerificationAsync("登录到GoodPass");
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
}