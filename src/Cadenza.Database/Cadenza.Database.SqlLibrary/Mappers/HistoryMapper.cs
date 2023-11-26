using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class HistoryMapper : IHistoryMapper
{
    public NowPlayingUpdateDTO MapNowPlaying(GetNowPlayingUpdatesResult data)
    {
        return new NowPlayingUpdateDTO
        {
            UserId = data.UserId,
            SessionKey = data.SessionKey,
            Timestamp = data.Timestamp,
            SecondsRemaining = data.SecondsRemaining,
            Track = data.Track,
            Artist = data.Artist,
            Album = data.Album,
            AlbumArtist = data.AlbumArtist
        };
    }

    public RecentAlbumDTO MapRecentAlbum(GetRecentAlbumsResult data)
    {
        return new RecentAlbumDTO
        {
            Id = data.AlbumId,
            Title = data.AlbumTitle,
            ArtistName = data.ArtistName
        };
    }

    public NewScrobbleDTO MapScrobble(GetNewScrobblesResult data)
    {
        return new NewScrobbleDTO
        {
            Id = data.Id,
            SessionKey = data.SessionKey,
            ScrobbledAt = data.ScrobbledAt,
            Track = data.Track,
            Artist = data.Artist,
            Album = data.Album,
            AlbumArtist = data.AlbumArtist
        };
    }
}
