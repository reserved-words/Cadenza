namespace Cadenza.API.LastFM.Interfaces;

internal interface IHasher
{
    string MD5Hash(string text);
}
