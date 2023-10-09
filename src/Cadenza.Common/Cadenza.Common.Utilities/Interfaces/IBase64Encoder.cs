namespace Cadenza.Common.Utilities.Interfaces;

public interface IBase64Encoder
{
    string Encode(string text);
    string Decode(string base64);
}
