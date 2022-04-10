using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Model.Id3;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local;

public class TrackConverter : ITrackConverter
{
    private readonly IBase64Converter _base64Converter;
    private readonly IIdGenerator _idGenerator;
    private readonly ICommentProcessor _commentProcessor;

    public TrackConverter(IBase64Converter base64Converter, IIdGenerator idGenerator, ICommentProcessor commentProcessor)
    {
        _base64Converter = base64Converter;
        _idGenerator = idGenerator;
        _commentProcessor = commentProcessor;
    }

    public TrackInfo ToAppModel(JsonTrack track, ICollection<JsonArtist> artists)
    {
        var artist = artists.Single(a => a.Id == track.ArtistId);

        return new TrackInfo
        {
            Id = track.Source == LibrarySource.Local ? _base64Converter.ToBase64(track.Path) : track.Path,
            Source = track.Source,
            ArtistId = track.ArtistId,
            ArtistName = artist.Name,
            AlbumId = track.AlbumId,
            Title = track.Title,
            DurationSeconds = track.DurationSeconds,
            Year = track.Year,
            Lyrics = track.Lyrics,
            Tags = track.Tags?.Split("|")
                .ToList()
        };
    }

    public JsonTrack ToJsonModel(TrackInfo track)
    {
        return new JsonTrack
        {
            Path = track.Source == LibrarySource.Local ? _base64Converter.FromBase64(track.Id) : track.Id,
            Source = track.Source,
            ArtistId = track.ArtistId,
            AlbumId = track.AlbumId,
            Title = track.Title,
            DurationSeconds = track.DurationSeconds,
            Year = Nullify(track.Year),
            Lyrics = Nullify(track.Lyrics),
            Tags = track.Tags == null
                ? null
                : Nullify(string.Join("|", track.Tags))
        };
    }

    public JsonTrack ToJsonModel(Id3Data data)
    {
        var commentData = _commentProcessor.GetData(data.Track.Comment);

        return new JsonTrack
        {
            Path = data.Track.Filepath,
            ArtistId = _idGenerator.GenerateId(data.Artist.Name),
            AlbumId = _idGenerator.GenerateId(data.Album.ArtistName, data.Album.Title),
            Title = data.Track.Title,
            DurationSeconds = (int)data.Track.Duration.TotalSeconds,
            Year = Nullify(commentData.TrackYear),
            Lyrics = Nullify(data.Track.Lyrics),
            Tags = commentData.Tags == null
                ? null
                : Nullify(string.Join("|", commentData.Tags))
        };
    }

    private string Nullify(string text)
    {
        return string.IsNullOrWhiteSpace(text)
            ? null
            : text;
    }
}