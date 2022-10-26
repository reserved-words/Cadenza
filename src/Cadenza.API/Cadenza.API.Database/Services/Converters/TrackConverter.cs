namespace Cadenza.API.Database.Services.Converters;

internal class TrackConverter : ITrackConverter
{
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
            Tags = new TagList(track.Tags)
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
            Tags = track.Tags.ToString().Nullify()
        };
    }
}