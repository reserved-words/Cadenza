using Cadenza.Common.LastFm.Model;

namespace Cadenza.Common.LastFm;

public interface ILastFmUserService
{
    Task ScrobbleTrack(string sessionKey, Scrobble scrobble);
    Task UpdateLovedTrack(string sessionKey, LovedTrack track);
    Task UpdateNowPlaying(string sessionKey, NowPlaying nowPlaying);
}
