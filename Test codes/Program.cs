var saltchar = new char[8];
var saltbase = new int[8] { 39, 65, 110, 78, 95, 102, 116, 43 };
var InputString = "test";
for (var i = 0; i < 8; i++)
{
    saltchar[i] = Convert.ToChar(saltbase[i]);

}
Console.WriteLine(saltchar);
var salt = new string(saltchar);
Console.WriteLine(salt);
var salthead = salt[..4];
var salttail = salt[4..];
var SaltedString = salthead + InputString + salttail;
Console.WriteLine(SaltedString);