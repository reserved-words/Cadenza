namespace Cadenza.Web.Database.Interfaces;

internal interface IUrl
{
    string Build(string endpoint, params (string, object)[] parameters);
    string Build(string baseUrl, string endpoint, params (string, object)[] parameters);
}
