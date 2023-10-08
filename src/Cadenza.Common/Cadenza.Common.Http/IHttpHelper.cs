using Cadenza.Common.Model;

namespace Cadenza.Common.Http;

public interface IHttpHelper
{
    Task Delete(string url, object data = null);

    Task<T1> Get<T1>(string url) where T1 : new();
    Task<string> Get(string url);

    Task<ArtworkImage> GetImage(string url);

    Task Post(string url, Dictionary<string, string> parameters);
    Task Post(string url, object data = null);

    Task Put(string url, object data = null);
}
