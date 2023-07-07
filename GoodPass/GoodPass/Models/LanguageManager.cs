using System.Diagnostics;
using System.Globalization;

namespace GoodPass.Models;

public class Language
{
    #region Properties
    public CultureInfo Culture
    {
        get;
    }

    public string InteralName
    {
        get;
    }

    public string DisplayName
    {
        get;
    }

    #endregion

    #region Methods

    public Language()
    {
        Culture = new CultureInfo(Windows.System.UserProfile.GlobalizationPreferences.Languages[0]);
        Debug.WriteLine(Culture.DisplayName);
        DisplayName = Culture.DisplayName;
        InteralName = Culture.Name;
    }

    public Language(CultureInfo cultureInfo)
    {
        Culture = cultureInfo;
        DisplayName = Culture.DisplayName;
        InteralName = Culture.Name;
    }

    #endregion
}
public class LanguageManager
{
    #region Properties

    public List<Language> AvaliableLanguages
    {
        get;
    }

    public Language CurrentLanguage
    {
        get;
    }

    #endregion

    #region Methods

    public LanguageManager()
    {
        AvaliableLanguages = new List<Language>
        {
            new Language(new CultureInfo("en-US")),
            new Language(new CultureInfo("zh-CN"))
        };
        CurrentLanguage = new Language();
    }

    #endregion
}
