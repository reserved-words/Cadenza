using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Core.Services;

internal class EditItemMapper : IEditItemMapper
{
    public EditableAlbum MapAlbum(AlbumDetailsVM album)
    {
        return new EditableAlbum
        {
            Id = album.Id,
            ArtistId = album.ArtistId,
            ArtistName = album.ArtistName,
            Title = album.Title,
            ReleaseType = album.ReleaseType,
            Year = album.Year,
            ArtworkBase64 = album.ArtworkBase64,
            Tags = album.Tags.ToList()
        };
    }

    public List<EditableAlbumDisc> MapAlbumTracks(IReadOnlyCollection<AlbumDiscVM> tracks)
    {
        return tracks
            .Select(d => new EditableAlbumDisc
            {
                DiscNo = d.DiscNo,
                TrackCount = d.TrackCount,
                Tracks = d.Tracks.Select(t => new EditableAlbumTrack
                {
                    TrackId = t.TrackId,
                    TrackNo = t.TrackNo,
                    DiscNo = t.DiscNo,
                    Title = t.Title,
                    DurationSeconds = t.DurationSeconds,
                    ArtistId = t.ArtistId,
                    ArtistName = t.ArtistName,
                    IdFromSource = t.IdFromSource
                }).ToList()
            })
            .ToList();
    }

    public EditableArtist MapArtist(ArtistDetailsVM artist)
    {
        return new EditableArtist
        {
            Id = artist.Id,
            Name = artist.Name,
            Grouping = artist.Grouping,
            Genre = artist.Genre,
            Country = artist.Country,
            State = artist.State,
            City = artist.City,
            ImageBase64 = artist.ImageBase64,
            Tags = artist.Tags.ToList()
        };
    }

    public List<EditableArtistRelease> MapArtistReleases(IReadOnlyCollection<AlbumVM> releases)
    {
        return releases
            .Select(a => new EditableArtistRelease
            {
                Id = a.Id,
                Title = a.Title,
                ReleaseType = a.ReleaseType,
                Year = a.Year
            })
            .ToList();
    }

    public EditableTrack MapTrack(TrackDetailsVM track)
    {
        return new EditableTrack
        {
            Id = track.Id,
            ArtistId = track.ArtistId,
            ArtistName = track.ArtistName,
            Title = track.Title,
            Year = track.Year,
            AlbumId = track.AlbumId,
            DurationSeconds = track.DurationSeconds,
            IdFromSource = track.IdFromSource,
            Lyrics = track.Lyrics,
            Source = track.Source,
            Tags = track.Tags.ToList()
        };
    }
}
