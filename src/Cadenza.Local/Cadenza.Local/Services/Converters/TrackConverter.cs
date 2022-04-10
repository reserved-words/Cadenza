using Cadenza.Local.Common.Interfaces;
using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Services.Converters;

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
            Year = track.Year.Nullify(),
            Lyrics = track.Lyrics.Nullify(),
            Tags = track.Tags == null
                ? null
                : string.Join("|", track.Tags).Nullify()
        };
    }
}