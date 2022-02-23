namespace Cadenza.Core;

public interface IUrl
{
    string Build(string baseUrl, string endpoint, params (string, object)[] parameters);
}
