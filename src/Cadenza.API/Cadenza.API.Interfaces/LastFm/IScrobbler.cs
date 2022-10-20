namespace Cadenza.API.Interfaces.LastFm;

public interface IScrobbler
{
    Task RecordPlay(LFM_Scrobble scrobble);
    Task UpdateNowPlaying(LFM_Scrobble scrobble);
}