namespace Cadenza.Common.Interfaces.Utilities;

public interface IBase64Encoder
{
    string Encode(string text);
    string Decode(string base64);
}
