using GoodPass.Services;

var ts = new MasterKeyService();
bool re = ts.SetLocalMKHash("0000");
Console.WriteLine(re);