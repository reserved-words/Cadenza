namespace Cadenza.LastFM;

public interface ILastFmConfig
{
    string ApiKey { get; }
    string Username { get; }
    string ApiSecret { get; }
    string ApiBaseUrl { get; }
    string AuthUrl { get; }
}
