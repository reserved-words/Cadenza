namespace Cadenza.Common;

public interface IHasher
{
    Task<string> MD5Hash(string text);
}
