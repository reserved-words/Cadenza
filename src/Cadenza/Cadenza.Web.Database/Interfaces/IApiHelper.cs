namespace Cadenza.Web.Database.Interfaces;

internal interface IApiHelper
{
    Task<T> Get<T>(string endpoint);
    Task<T> Get<T>(string endpoint, string id);
    Task Post<T>(string endpoint, T data);
}