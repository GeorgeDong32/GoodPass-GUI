namespace GoodPass.Helpers;

public static class TaskTConverter
{
    public static async Task<string> StringToTaskString(string str)
    {
        return await Task.FromResult(str);
    }
}