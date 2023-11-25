using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Search;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class SearchMapper : ISearchMapper
{
    public SearchItemDTO MapAlbum(GetAlbumsResult result)
    {
        return new SearchItemDTO
        {
            Type = PlayerItemType.Album,
            Id = result.Id.ToString(),
            Name = result.Title,
            Artist = result.ArtistName
        };
    }

    public SearchItemDTO MapArtist(GetArtistsResult result)
    {
        return new SearchItemDTO
        {
            Type = PlayerItemType.Artist,
            Id = result.Id.ToString(),
            Name = result.Name
        };
    }

    public SearchItemDTO MapGenre(GetGenresResult result)
    {
        return new SearchItemDTO
        {
            Type = PlayerItemType.Genre,
            Id = result.Genre,
            Name = result.Genre
        };
    }

    public SearchItemDTO MapGrouping(GetGroupingsResult result)
    {
        return new SearchItemDTO
        {
            Type = PlayerItemType.Grouping,
            Id = result.Id.ToString(),
            Name = result.Name
        };
    }

    public SearchItemDTO MapTag(GetTagsResult result)
    {
        return new SearchItemDTO
        {
            Type = PlayerItemType.Tag,
            Id = result.Tag,
            Name = result.Tag
        };
    }

    public SearchItemDTO MapTrack(GetTracksResult result)
    {
        return new SearchItemDTO
        {
            Type = PlayerItemType.Track,
            Id = result.Id.ToString(),
            Name = result.Title,
            Artist = result.ArtistName,
            Album = result.AlbumTitle,
            AlbumDisplay = result.AlbumTitle + (result.AlbumArtistName == result.ArtistName ? "" : $" ({result.AlbumArtistName})")
        };
    }
}
