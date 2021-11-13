namespace Cadenza.Common;

public interface IBase64Converter
{
    string ToImageSrc(byte[] bytes);
    string ToBase64(string text);
    string FromBase64(string base64);
}
