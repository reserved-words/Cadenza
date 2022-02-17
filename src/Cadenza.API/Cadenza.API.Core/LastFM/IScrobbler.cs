namespace Cadenza.API.Core.LastFM;

public interface IScrobbler
{
    Task RecordPlay(Scrobble scrobble);
    Task UpdateNowPlaying(Scrobble scrobble);
}