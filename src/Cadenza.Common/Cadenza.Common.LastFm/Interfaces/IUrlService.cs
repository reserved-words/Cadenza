namespace Cadenza.Common.LastFm.Interfaces;

internal interface IUrlService
{
    string AddParameters(string url, Dictionary<string, string> parameters);
}