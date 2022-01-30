namespace Cadenza.API.Wrapper;

internal interface IUrl
{
    string Build(string endpoint, params (string, object)[] parameters);
}
