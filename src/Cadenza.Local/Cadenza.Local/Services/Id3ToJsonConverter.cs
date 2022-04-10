using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Model.Id3;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Services;

public class Id3ToJsonConverter : IId3ToJsonConverter
{
    private readonly IIdGenerator _idGenerator;
    private readonly ICommentProcessor _commentProcessor;

    public Id3ToJsonConverter(IIdGenerator idGenerator, ICommentProcessor commentProcessor)
    {
        _idGenerator = idGenerator;
        _commentProcessor = commentProcessor;
    }

    public JsonAlbum ConvertAlbum(Id3Data data)
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
            Year = data.Album.Year.Nullify()
        };

        var discNo = data.Disc.DiscNo == 0 ? 1 : data.Disc.DiscNo;

        while (jsonAlbum.TrackCounts.Count < discNo)
        {
            jsonAlbum.TrackCounts.Add(0);
        }

        jsonAlbum.TrackCounts[discNo - 1] = data.Disc.TrackCount;

        return jsonAlbum;

    }

    public JsonArtist ConvertAlbumArtist(Id3Data data)
    {
        return new JsonArtist
        {
            Id = _idGenerator.GenerateId(data.Album.ArtistName),
            Name = data.Album.ArtistName
        };
    }

    public JsonAlbumTrackLink ConvertAlbumTrackLink(Id3Data id3Data)
    {
        // need a single place to do these conversions

        var albumId = _idGenerator.GenerateId(id3Data.Album.ArtistName, id3Data.Album.Title);

        return new JsonAlbumTrackLink
        {
            TrackPath = id3Data.Track.Filepath,
            AlbumId = albumId,
            DiscNo = id3Data.Disc.DiscNo,
            TrackNo = id3Data.Track.TrackNo
        };
    }

    public JsonTrack ConvertTrack(Id3Data data)
    {
        var commentData = _commentProcessor.GetData(data.Track.Comment);

        return new JsonTrack
        {
            Path = data.Track.Filepath,
            ArtistId = _idGenerator.GenerateId(data.Artist.Name),
            AlbumId = _idGenerator.GenerateId(data.Album.ArtistName, data.Album.Title),
            Title = data.Track.Title,
            DurationSeconds = (int)data.Track.Duration.TotalSeconds,
            Year = commentData.TrackYear.Nullify(),
            Lyrics = data.Track.Lyrics.Nullify(),
            Tags = commentData.Tags == null
                ? null
                : string.Join("|", commentData.Tags).Nullify()
        };
    }

    public JsonArtist ConvertTrackArtist(Id3Data data)
    {
        var commentData = _commentProcessor.GetData(data.Track.Comment);

        return new JsonArtist
        {
            Id = _idGenerator.GenerateId(data.Artist.Name),
            Name = data.Artist.Name,
            Grouping = data.Artist.Grouping.ToString(),
            Genre = data.Artist.Genre.Nullify(),
            City = commentData.City.Nullify(),
            State = commentData.State.Nullify(),
            Country = commentData.Country.Nullify()
        };
    }
}