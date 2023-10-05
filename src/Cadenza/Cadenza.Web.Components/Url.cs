namespace Cadenza.Web.Components;

internal class Url : IUrl
{
    public string Build(string baseUrl, string endpoint, params (string, object)[] parameters)
    {
        var url = $"{baseUrl}{endpoint}";
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