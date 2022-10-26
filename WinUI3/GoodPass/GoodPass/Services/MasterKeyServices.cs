using GoodPass.Contracts.Services;
using GoodPass.Core.Contracts.Services;
using GoodPass.Core.Helpers;
using GoodPass.Helpers;
using GoodPass.Models;

using Microsoft.Extensions.Options;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using System;

namespace GoodPass.Services;

public class MasterKeyService : IMaterKeyService
{
    private string _LocalMKHash;
    readonly string userName;

    readonly string _appdataPath;

    private readonly string _LocalMKPath; 

    public MasterKeyService()
    {
        userName = Environment.UserName;
        _appdataPath = $"C:\\Users\\{userName}\\AppData\\Local";
        _LocalMKPath = Path.Combine(_appdataPath, "GoodPass", "MKconfig.txt");
        _LocalMKHash = "";
    }

    public string GetLocalMKHash()
    {
        var LocalMKHash = "";
        try
        {
            LocalMKHash = File.ReadAllText(_LocalMKPath);
        }
        catch (System.IO.DirectoryNotFoundException)
        {
            _LocalMKHash = "Not found";
            LocalMKHash = "Not found";
        }
        catch (System.IO.FileNotFoundException)
        {
            _LocalMKHash = "Not found";
            LocalMKHash = "Not found";
        }
        finally
        {
            if (LocalMKHash == "")
                _LocalMKHash = "Empty";
            else
                _LocalMKHash = LocalMKHash;
        }
        return _LocalMKHash;
    }

    public string CheckMasterKey(string InputKey)
    {
        GetLocalMKHash();
        if (InputKey == _LocalMKHash)
            return "pass";
        else if (_LocalMKHash == "Not found")
            return "error: not found";
        else if (_LocalMKHash == "Empty")
            return "error: data broken";
        else if (InputKey != _LocalMKHash)
            return "npass";
        else return "Unknown Error";
    }

    public void ProcessMKArray(string InputKey)
    {
        App.EncryptBase = new int[40] { 1, 4, 1, 5, 9, 2, 6, 5, 3, 5, 8, 9, 7, 9, 3, 2, 3, 8, 4, 6, 2, 6, 4, 3, 3, 8, 3, 2, 7, 9, 5, 0, 2, 8, 8, 4, 1, 9, 7, 1 };
        App.MKBase = App.EncryptBase;
    }

}
