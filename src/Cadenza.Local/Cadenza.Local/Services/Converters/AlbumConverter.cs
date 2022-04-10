using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Model.Id3;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Services.Converters;

public class AlbumConverter : IAlbumConverter
{
    private readonly IIdGenerator _idGenerator;

    public AlbumConverter(IIdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

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
            Year = Nullify(album.Year),
            ArtworkUrl = album.ArtworkUrl
        };

        return jsonAlbum;
    }

    public JsonAlbum ToJsonModel(Id3Data data)
    {
        var albumId = _idGenerator.GenerateId(data.Album.ArtistName, data.Album.Title);
        var artistId = _idGenerator.GenerateId(data.Album.ArtistName);

        var jsonAlbum = new JsonAlbum
        {
            Id = albumId,
            ArtistId = artistId,
            Title = data.Album.Title,
            ReleaseType = data.Album.ReleaseType ?? ReleaseType.Album.ToString(),
            TrackCounts = new List<int>(),
            Year = Nullify(data.Album.Year)
        };

        var discNo = data.Disc.DiscNo == 0 ? 1 : data.Disc.DiscNo;

        while (jsonAlbum.TrackCounts.Count < discNo)
        {
            jsonAlbum.TrackCounts.Add(0);
        }

        jsonAlbum.TrackCounts[discNo - 1] = data.Disc.TrackCount;

        return jsonAlbum;
    }

    private string Nullify(string text)
    {
        return string.IsNullOrWhiteSpace(text)
            ? null
            : text;
    }
}
