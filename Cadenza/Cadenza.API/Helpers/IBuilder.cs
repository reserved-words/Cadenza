namespace Cadenza.API;

public interface IBuilder
{
    string BuildAuthHeader(string clientId, string clientSecret);
    string BuildUrl(string baseUrl, params (string Name, string Value)[] queryParameters);
}
