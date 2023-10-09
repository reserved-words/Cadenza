using Cadenza.Common.Utilities.Interfaces;
using System.Text;

namespace Cadenza.Common.Utilities.Services;

internal class Base64Encoder : IBase64Encoder
{
    public string Decode(string base64)
    {
        var textBytes = Convert.FromBase64String(base64);
        return Encoding.UTF8.GetString(textBytes);
    }

    public string Encode(string text)
    {
        var textBytes = Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(textBytes);
    }
}