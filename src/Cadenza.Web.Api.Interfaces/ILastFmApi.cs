namespace Cadenza.Web.Api.Interfaces;

public interface ILastFmApi
{
    Task<string> GetAuthUrl(string redirectUri);
    Task CreateSession(string token);
    Task<bool> DoesSessionExist();
}
