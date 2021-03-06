using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Services.Converters;

public class AlbumConverter : IAlbumConverter
{
    public AlbumInfo ToAppModel(JsonAlbum album, ICollection<JsonArtist> artists)
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

    public JsonAlbum ToJsonModel(AlbumInfo album)
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
