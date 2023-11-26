using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;

internal interface IHistoryMapper
{
    RecentAlbumDTO MapRecentAlbum(GetRecentAlbumsResult data);
    NewScrobbleDTO MapScrobble(GetNewScrobblesResult data);
    NowPlayingUpdateDTO MapNowPlaying(GetNowPlayingUpdatesResult data);
    RecentTrackDTO MapRecentTrack(GetRecentTracksResult result);
}
