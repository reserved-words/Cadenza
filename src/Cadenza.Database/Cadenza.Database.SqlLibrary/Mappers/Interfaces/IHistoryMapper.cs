using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;

internal interface IHistoryMapper
{
    RecentAlbumDTO MapRecentAlbum(GetRecentAlbumsResult data);
    RecentTrackDTO MapRecentTrack(GetRecentTracksResult result);
}
