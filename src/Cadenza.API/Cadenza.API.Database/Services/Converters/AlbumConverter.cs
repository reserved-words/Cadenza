namespace Cadenza.API.Database.Services.Converters;

internal class AlbumConverter : IAlbumConverter
{
    public AlbumInfo ToModel(JsonAlbum album, ICollection<JsonArtist> artists)
    {
        return new AlbumInfo
        {
            Source = album.Source,
            Id = album.Id,
            ArtistId = album.ArtistId,
            ArtistName = artists.Single(a => a.Id == album.ArtistId).Name,
            Title = album.Title,
            ReleaseType = album.ReleaseType.Parse<ReleaseType>(ReleaseType.Album),
            Year = album.Year,
            DiscCount = album.TrackCounts.Count,
            TrackCounts = album.TrackCounts,
            ArtworkUrl = album.ArtworkUrl,
            Tags = new TagList(album.Tags)
        };
    }

    public JsonAlbum ToJson(AlbumInfo album)
    {
        var jsonAlbum = new JsonAlbum
        {
            Source = album.Source,
            Id = album.Id,
            ArtistId = album.ArtistId,
            Title = album.Title,
            ReleaseType = album.ReleaseType.ToString(),
            TrackCounts = album.TrackCounts,
            Year = album.Year.Nullify(),
            ArtworkUrl = album.ArtworkUrl.Nullify(),
            Tags = album.Tags.ToString().Nullify()
        };

        return jsonAlbum;
    }
}
