namespace Cadenza.Web.Info.Interfaces;

internal interface IWebInfoHttpHelper 
{
    Task<T1> Get<T1>(string url) where T1 : new();
    Task<string> Get(string url);
}
