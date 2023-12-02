namespace Cadenza.Web.Common.Interfaces;

public interface ILastFmSessionService
{
    Task<string> GetAuthUrl(string redirectUri);
    Task CreateSession(string token);
    Task<bool> DoesSessionExist();
}
