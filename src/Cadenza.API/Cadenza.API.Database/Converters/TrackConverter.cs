using Cadenza.API.Database.Interfaces.Converters;
using Cadenza.API.Database.Model;
using Cadenza.Domain.Enums;
using Cadenza.Domain.Extensions;
using Cadenza.Domain.Models.Track;
using Cadenza.Utilities.Interfaces;

namespace Cadenza.API.Database.Converters;

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
            Id = track.Id,
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

    public JsonTrack ToJson(TrackInfo track)
    {
        return new JsonTrack
        {
            Id = track.Id,
            Path = track.Source == LibrarySource.Local ? _base64Converter.FromBase64(track.Id) : null,
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