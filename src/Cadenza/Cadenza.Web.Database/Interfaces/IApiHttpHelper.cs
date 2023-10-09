using Cadenza.Common.Http.Interfaces;

namespace Cadenza.Web.Database.Interfaces;

public interface IApiHttpHelper : IHttpHelper
{
    Task<T> Get<T>(string url, object id) where T : new();
}