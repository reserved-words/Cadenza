namespace Cadenza.LastFM;

public interface ILastFmAuth
{
    string GetAuthUrl(string redirectUri);
    string GetSessionKeyUrl(string token);
}