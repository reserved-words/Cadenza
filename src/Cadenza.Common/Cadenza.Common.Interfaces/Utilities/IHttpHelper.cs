namespace Cadenza.Common.Interfaces.Utilities;

public interface IHttpHelper
{
    Task<HttpResponseMessage> Get(string url, string authHeader = null);
    Task<T> Get<T>(string url, string authHeader = null);
    Task Post(string url, string authHeader = null, object data = null);
    Task Put(string url, string authHeader = null, object data = null);
    Task Delete(string url, string authHeader = null, object data = null);
    Task Post(string url, string authHeader = null, Dictionary<string, string> parameters = null);
}
