namespace Cadenza.Common.LastFm.Services;

internal class UrlService : IUrlService
{
    public string AddParameters(string url, Dictionary<string, string> parameters)
    {
        var pairs = parameters.Select(p => $"{p.Key}={HttpUtility.UrlEncode(p.Value)}");
        var queryString = string.Join("&", pairs);
        return $"{url}?{queryString}";
    }
}
