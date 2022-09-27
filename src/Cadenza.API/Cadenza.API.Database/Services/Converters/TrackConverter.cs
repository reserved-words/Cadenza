using Cadenza.Domain.Model.Track;

namespace Cadenza.API.Database.Services.Converters;

internal class TrackConverter : ITrackConverter
{
    private readonly IBase64Converter _base64Converter;

    public TrackConverter(IBase64Converter base64Converter)
    {
        _base64Converter = base64Converter;
    }

    public TrackInfo ToModel(JsonTrack track, ICollection<JsonArtist> artists)
    {
        var artist = artists.Single(a => a.Id == track.ArtistId);

        return new TrackInfo
        {
            Source = track.Source,
            Id = track.Id,
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

    public JsonTrack ToJson(TrackInfo track)
    {
        return new JsonTrack
        {
            Source = track.Source,
            Id = track.Id,
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