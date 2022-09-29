namespace Cadenza.Web.Common.Interop;

public interface IStore
{
    Task<string> GetValue(string key);
    Task SetValue(string key, string value);
}