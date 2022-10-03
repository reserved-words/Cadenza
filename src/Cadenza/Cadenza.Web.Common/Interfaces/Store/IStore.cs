namespace Cadenza.Web.Common.Interfaces.Store;

public interface IStore
{
    Task<string> GetValue(string key);
    Task SetValue(string key, string value);
}