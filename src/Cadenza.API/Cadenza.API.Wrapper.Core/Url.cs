using Microsoft.Extensions.Options;
using System.Web;

namespace Cadenza.API.Wrapper.Core;

public class Url : IUrl
{
    private readonly ApiSettings _api;

    public Url(IOptions<ApiSettings> api)
    {
        _api = api.Value;
    }

    public string Build(string endpoint, params (string, object)[] parameters)
    {
        var url = $"{_api.BaseUrl}{endpoint}";
        var keyValuePairs = parameters.Select(GetQueryPair);
        var queryString = string.Join("&", keyValuePairs);
        return $"{url}?{queryString}";
    }

    private string GetQueryPair((string, object) parameter)
    {
        var key = parameter.Item1;

        var value = parameter.Item2 == null
            ? null
            : parameter.Item2 is string strValue
            ? HttpUtility.UrlEncode(strValue)
            : parameter.Item2.ToString();

        return $"{key}={value}";
    }
}