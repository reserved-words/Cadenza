namespace Cadenza.Web.Database.Interfaces;

internal interface IApiHelper
{
    Task<T> Get<T>(string endpoint);
    Task<T> Get<T>(string endpoint, int id);
    Task<T> Get<T>(string endpoint, string id);
    Task Post(string endpoint);
    Task Post<T>(string endpoint, T data);
}