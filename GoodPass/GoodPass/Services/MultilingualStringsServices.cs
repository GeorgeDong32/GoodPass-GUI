using GoodPass.Strings;

namespace GoodPass.Services;

public class MultilingualStringsServices
{
    private readonly UIStrings UIStrings_zh_CN;

    private readonly UIStrings UIStrings_en_US;

    public MultilingualStringsServices()
    {
        UIStrings_zh_CN = new UIStrings("zh-CN");
        UIStrings_en_US = new UIStrings("en-US");
    }
}