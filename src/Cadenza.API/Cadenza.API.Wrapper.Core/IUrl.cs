namespace Cadenza.API.Wrapper.Core;

public interface IUrl
{
    string Build(string endpoint, params (string, object)[] parameters);
}
