using Cadenza.Domain;
using Cadenza.Local.Common.Model.Json;
using Cadenza.Local.MusicFiles.Interfaces;
using Cadenza.Local.MusicFiles.Model;
using Cadenza.Utilities;

namespace Cadenza.Local.MusicFiles.Services;

internal class Id3ToJsonConverter : IId3ToJsonConverter
{
    private readonly IBase64Converter _base64Converter;
    private readonly IIdGenerator _idGenerator;
    private readonly ICommentProcessor _commentProcessor;

    public Id3ToJsonConverter(IIdGenerator idGenerator, ICommentProcessor commentProcessor, IBase64Converter base64Converter)
    {
        _idGenerator = idGenerator;
        _commentProcessor = commentProcessor;
        _base64Converter = base64Converter;
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

    public JsonAlbumTrackLink ConvertAlbumTrackLink(string id, Id3Data id3Data)
    {
        var albumId = _idGenerator.GenerateId(id3Data.Album.ArtistName, id3Data.Album.Title);

        return new JsonAlbumTrackLink
        {
            TrackId = id,
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
            Id = _base64Converter.ToBase64(data.Track.Filepath),
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