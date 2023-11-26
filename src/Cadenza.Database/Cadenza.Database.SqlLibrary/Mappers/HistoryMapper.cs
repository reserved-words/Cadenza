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

    public NewScrobbleDTO MapScrobble(GetNewScrobblesResult result)
    {
        return new NewScrobbleDTO
        {
            Id = result.Id,
            SessionKey = result.SessionKey,
            ScrobbledAt = result.ScrobbledAt,
            Track = result.Track,
            Artist = result.Artist,
            Album = result.Album,
            AlbumArtist = result.AlbumArtist
        };
    }
}
