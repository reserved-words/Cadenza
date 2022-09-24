using Cadenza.Domain.Models.LastFm;

namespace Cadenza.API.LastFM;

public interface IScrobbler
{
    Task RecordPlay(Scrobble scrobble);
    Task UpdateNowPlaying(Scrobble scrobble);
}