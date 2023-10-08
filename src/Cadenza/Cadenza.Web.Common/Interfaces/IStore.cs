namespace Cadenza.Web.Common.Interfaces;

public interface IStore
{
    Task<string> GetValue(string key);
    Task SetValue(string key, string value);
}