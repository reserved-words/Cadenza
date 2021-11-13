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
            Id = _base64Converter.ToBase64(track.Path),
            ArtistId = track.ArtistId,
            ArtistName = artist.Name,
            Title = track.Title,
            DurationSeconds = track.DurationSeconds,
            Year = track.Year,
            Lyrics = track.Lyrics,
            Tags = track.Tags?.Split("|")
                .Select(t => new Tag { Value = t })
                .ToList()
        };
    }

    public JsonTrack ToJsonModel(TrackInfo track)
    {
        return new JsonTrack
        {
            Path = _base64Converter.FromBase64(track.Id),
            ArtistId = track.ArtistId,
            Title = track.Title,
            DurationSeconds = track.DurationSeconds,
            Year = Nullify(track.Year),
            Lyrics = Nullify(track.Lyrics),
            Tags = track.Tags == null
                ? null
                : Nullify(string.Join("|", track.Tags.Select(t => t.Value)))
        };
    }

    public JsonTrack ToJsonModel(Id3Data data)
    {
        var commentData = _commentProcessor.GetData(data.Track.Comment);

        return new JsonTrack
        {
            Path = data.Track.Filepath,
            ArtistId = _idGenerator.GenerateArtistId(data.Artist.Name),
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