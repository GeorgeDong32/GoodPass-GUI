// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml.Controls;

namespace GoodPass.Controls;

public sealed class GPPasswordBox : TextBox
{
    private string password;

    public string RevealText;

    public GPPasswordBox(string password)
    {
        this.DefaultStyleKey = typeof(GPPasswordBox);
        this.password = password;
        RevealText = string.Empty;
        var length = password.Length;
        for (var i = 0; i < length; i++)
        {
            RevealText += '•';
        }
        Text = RevealText;
    }
}
