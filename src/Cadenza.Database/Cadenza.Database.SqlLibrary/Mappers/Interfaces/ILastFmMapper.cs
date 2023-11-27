using Cadenza.Database.SqlLibrary.Model.LastFm;

namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;

internal interface ILastFmMapper
{
    NewScrobbleDTO MapScrobble(GetNewScrobblesResult data);
    NowPlayingUpdateDTO MapNowPlaying(GetNowPlayingUpdatesResult data);
    LovedTrackUpdateDTO MapLovedTrack(GetLovedTrackUpdatesResult data);
}
