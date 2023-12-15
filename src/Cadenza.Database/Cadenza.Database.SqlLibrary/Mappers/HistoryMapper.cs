using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.History;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class HistoryMapper : IHistoryMapper
{
    public RecentAlbumDTO MapRecentlyAddedAlbum(GetRecentlyAddedAlbumsResult data)
    {
        return new RecentAlbumDTO
        {
            Id = data.AlbumId,
            Title = data.AlbumTitle,
            ArtistName = data.ArtistName
        };
    }

    public RecentAlbumDTO MapRecentlyPlayedAlbum(GetRecentlyPlayedAlbumsResult data)
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

    public TopAlbumDTO MapTopAlbum(GetTopAlbumsResult result, int rank)
    {
        return new TopAlbumDTO
        {
            Id = result.Id,
            Title = result.Title,
            Artist = result.Artist,
            Plays = result.Plays,
            Rank = rank
        };
    }

    public TopArtistDTO MapTopArtist(GetTopArtistsResult result, int rank)
    {
        return new TopArtistDTO
        {
            Id = result.Id,
            Name = result.Name,
            Plays = result.Plays,
            Rank = rank
        };
    }

    public TopTrackDTO MapTopTrack(GetTopTracksResult result, int rank)
    {
        return new TopTrackDTO
        {
            Id = result.Id,
            Title = result.Title,
            Artist = result.Artist,
            Plays = result.Plays,
            Rank = rank
        };
    }
}
