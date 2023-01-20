namespace GoodPass.Models;

public enum AddDataResult
{
    Success,
    Failure_Duplicate,
    Failure,
    Undetermined
}

public enum EditDataResult
{
    Success,
    Failure,
    UnknowError,
    Nochange
}