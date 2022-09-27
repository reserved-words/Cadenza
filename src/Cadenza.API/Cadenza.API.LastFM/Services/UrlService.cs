using System.Web;

namespace Cadenza.API.LastFM.Services;

internal class UrlService : IUrlService
{
    public string SetMethod(string url, string name)
    {
        var encodedValue = HttpUtility.UrlEncode(name);
        return $"{url}?method={encodedValue}";
    }

    public string AddParameter(string url, string key, string value)
    {
        var encodedValue = HttpUtility.UrlEncode(value);
        return $"{url}&{key}={encodedValue}";
    }

    public string AddParameter(string url, string key, int value)
    {
        return $"{url}&{key}={value}";
    }

    public string AddParameters(string url, Dictionary<string, string> parameters)
    {
        var pairs = parameters.Select(p => $"{p.Key}={HttpUtility.UrlEncode(p.Value)}");
        var queryString = string.Join("&", pairs);
        return $"{url}?{queryString}";
    }
}
