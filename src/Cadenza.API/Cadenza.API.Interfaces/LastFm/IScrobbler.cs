namespace Cadenza.API.Interfaces.LastFm;

public interface IScrobbler
{
    Task UpdateNowPlaying(ScrobbleDTO scrobble);
}