namespace Cadenza.Web.Database.Interfaces;

internal interface IApiHttpHelper : IHttpHelper
{
    Task<T> Get<T>(string url, object id) where T : new(); 
}