using Cadenza.Utilities.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Cadenza.Utilities.Implementations;

internal class Hasher : IHasher
{
    public string MD5Hash(string input)
    {
        using var md5Hash = MD5.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var data = md5Hash.ComputeHash(bytes);
        var elements = data.Select(i => i.ToString("x2"));
        return string.Join("", elements);
    }
}