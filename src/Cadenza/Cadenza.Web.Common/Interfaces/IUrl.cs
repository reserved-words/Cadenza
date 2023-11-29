namespace Cadenza.Web.Common.Interfaces;

public interface IUrl
{
    string Build(string endpoint, params (string, object)[] parameters);
    string Build(string baseUrl, string endpoint, params (string, object)[] parameters);
}
