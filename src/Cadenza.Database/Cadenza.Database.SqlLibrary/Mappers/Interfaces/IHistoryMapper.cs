using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Mappers.Interfaces;

internal interface IHistoryMapper
{
    RecentAlbumDTO MapRecentlyAddedAlbum(GetRecentlyAddedAlbumsResult result);
    RecentAlbumDTO MapRecentlyPlayedAlbum(GetRecentlyPlayedAlbumsResult result);
    RecentTrackDTO MapRecentTrack(GetRecentTracksResult result);
    TopAlbumDTO MapTopAlbum(GetTopAlbumsResult result, int rank);
    TopArtistDTO MapTopArtist(GetTopArtistsResult result, int rank);
    TopTrackDTO MapTopTrack(GetTopTracksResult result, int rank);
}
