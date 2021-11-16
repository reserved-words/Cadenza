namespace Cadenza.LastFM;

public interface ILastFmConfig
{
    string Username { get; }
    string ApiKey { get; }
    string ApiSecret { get; }
    string SessionKey { get; }
}
