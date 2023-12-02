using Cadenza.Common.Http.Interfaces;

namespace Cadenza.Web.Api.Interfaces;

internal interface IApiHttpHelper : IHttpHelper
{
    Task<T> Get<T>(string url, object id) where T : new();
}