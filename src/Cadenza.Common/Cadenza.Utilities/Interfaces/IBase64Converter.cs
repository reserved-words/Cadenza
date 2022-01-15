namespace Cadenza.Utilities;

public interface IBase64Converter
{
    string ToBase64(string text);
    string FromBase64(string base64);
}
