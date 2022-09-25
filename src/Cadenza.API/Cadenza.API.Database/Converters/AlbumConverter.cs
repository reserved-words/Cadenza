using Cadenza.API.Database.Interfaces.Converters;
using Cadenza.API.Database.Model;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Extensions;
using Cadenza.Domain.Models.Album;

namespace Cadenza.API.Database.Converters;

internal class AlbumConverter : IAlbumConverter
{
    public AlbumInfo ToModel(JsonAlbum album, ICollection<JsonArtist> artists)
    {
        return new AlbumInfo
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            ArtistName = artists.Single(a => a.Id == album.ArtistId).Name,
            Title = album.Title,
            ReleaseType = album.ReleaseType.Parse<ReleaseType>(ReleaseType.Album),
            Year = album.Year,
            DiscCount = album.TrackCounts.Count,
            TrackCounts = album.TrackCounts,
            ArtworkUrl = album.ArtworkUrl
        };
    }

    public JsonAlbum ToJson(AlbumInfo album)
    {
        var jsonAlbum = new JsonAlbum
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            Title = album.Title,
            ReleaseType = album.ReleaseType.ToString(),
            TrackCounts = album.TrackCounts,
            Year = album.Year.Nullify(),
            ArtworkUrl = album.ArtworkUrl
        };

        return jsonAlbum;
    }
}
