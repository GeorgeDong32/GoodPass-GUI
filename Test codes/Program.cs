using GoodPass.Services;

string test = "0000";
GoodPassSHAServices ts = new GoodPassSHAServices();
var testhash = ts.getGPHES(test);
Console.WriteLine(testhash);
//DA931530AAB8C29D72ED0336AA90895BF7CE5C041C4C1042AFD1D07F5EE5D5E7