namespace Cadenza.API.LastFM;

public interface IScrobbler
{
    Task RecordPlay(Scrobble scrobble);
    Task UpdateNowPlaying(Scrobble scrobble);
}