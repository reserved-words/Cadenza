using Cadenza.Utilities;

namespace Cadenza.Spotify;

public class Builder : IBuilder
{
    private readonly IBase64Converter _base64;

    public Builder(IBase64Converter base64)
    {
        _base64 = base64;
    }

    public string BuildAuthHeader(string clientId, string clientSecret)
    {
        var credentials = $"{clientId}:{clientSecret}";
        var encodedCredentials = _base64.ToBase64(credentials);
        return $"Basic {encodedCredentials}";
    }

    public string BuildUrl(string baseUrl, params (string Name, string Value)[] queryParameters)
    {
        var queryElements = queryParameters.Select(q => $"{q.Name}={q.Value}");
        var queryString = string.Join("&", queryElements);
        var url = $"{baseUrl}?{queryString}";
        return url;
    }
}
