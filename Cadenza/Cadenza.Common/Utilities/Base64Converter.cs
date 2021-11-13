using System.Text;

namespace Cadenza.Common;

public class Base64Converter : IBase64Converter
{
    public string FromBase64(string base64)
    {
        var textBytes = Convert.FromBase64String(base64);
        return Encoding.UTF8.GetString(textBytes);
    }

    public string ToBase64(string text)
    {
        var textBytes = Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(textBytes);
    }

    public string ToImageSrc(byte[] bytes)
    {
        if (bytes == null)
            return "";

        var artworkBase64 = Convert.ToBase64String(bytes);
        return $"data:image/png;base64,{artworkBase64}";
    }
}