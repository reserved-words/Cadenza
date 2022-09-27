namespace Cadenza.API.LastFM.Interfaces;

internal interface IUrlService
{
    string AddParameter(string url, string key, int value);
    string AddParameter(string url, string key, string value);
    string AddParameters(string url, Dictionary<string, string> parameters);
    string SetMethod(string url, string name);
}