using GoodPass.Contracts.Services;
using GoodPass.Core.Contracts.Services;
using GoodPass.Core.Helpers;
using GoodPass.Helpers;
using GoodPass.Models;

using Microsoft.Extensions.Options;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;

namespace GoodPass.Services;

public class MasterKeyService : IMaterKeyService
{
    private string _LocalMKHash;

    private readonly string _LocalMKPath = "C:\\Users\\username\\AppData\\Local";

    public string GetLocalMKHash()
    {
        var LocalMKHash = File.ReadAllText(Path.Combine(_LocalMKPath,"GoodPass","MKconfig.txt"));
        _LocalMKHash = LocalMKHash;
        return LocalMKHash;
    }

    public string CheckMasterKey(string InputKey)
    {
        if (InputKey == _LocalMKHash)
            return "pass";
        else if (InputKey != _LocalMKHash)
            return "npass";
        else if (_LocalMKHash == "Not found")
            return "error: not found";
        else if (_LocalMKHash == "Empty")
            return "error: data broken";
        else return "Unknown Error";
    }

    public void ProcessMKArray(string InputKey)
    {

    }


}
