using Cadenza.Domain.Models.LastFm;

namespace Cadenza.API.Interfaces.LastFm;

public interface IScrobbler
{
    Task RecordPlay(Scrobble scrobble);
    Task UpdateNowPlaying(Scrobble scrobble);
}