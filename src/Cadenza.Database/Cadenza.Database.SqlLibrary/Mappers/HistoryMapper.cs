using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class HistoryMapper : IHistoryMapper
{
    public RecentAlbumDTO MapRecentAlbum(GetRecentAlbumsResult data)
    {
        return new RecentAlbumDTO
        {
            Id = data.AlbumId,
            Title = data.AlbumTitle,
            ArtistName = data.ArtistName
        };
    }

    public RecentTrackDTO MapRecentTrack(GetRecentTracksResult result)
    {
        return new RecentTrackDTO
        {
            Id = result.Id,
            Title = result.Title,
            Artist = result.Artist,
            Album = result.AlbumTitle,
            AlbumId = result.AlbumId,
            Played = result.ScrobbledAt,
            NowPlaying = result.NowPlaying,
            IsLoved = result.IsLoved
        };
    }
}
