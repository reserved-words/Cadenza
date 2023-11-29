using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;

internal interface IHistoryMapper
{
    RecentAlbumDTO MapRecentAlbum(GetRecentAlbumsResult result);
    RecentTrackDTO MapRecentTrack(GetRecentTracksResult result);
    TopAlbumDTO MapTopAlbum(GetTopAlbumsResult result, int rank);
    TopArtistDTO MapTopArtist(GetTopArtistsResult result, int rank);
    TopTrackDTO MapTopTrack(GetTopTracksResult result, int rank);
}
