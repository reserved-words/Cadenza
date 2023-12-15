using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Core.Services;

internal class EditItemMapper : IEditItemMapper
{
    public EditableAlbum MapEditableAlbum(AlbumDetailsVM album)
    {
        return new EditableAlbum
        {
            Id = album.Id,
            ArtistName = album.ArtistName,
            Title = album.Title,
            ReleaseType = album.ReleaseType,
            Year = album.Year,
            DiscCount = album.DiscCount,
            ArtworkBase64 = album.ArtworkBase64,
            Tags = album.Tags.ToList()
        };
    }

    public EditableAlbumDiscs MapEditableAlbumTracks(IReadOnlyCollection<AlbumTrackVM> tracks)
    {
        return new EditableAlbumDiscs
        {
            Discs = tracks
                .GroupBy(t => t.DiscNo)
                .Select(d => new EditableAlbumDisc
                {
                    DiscNo = d.Key,
                    TrackCount = d.First().DiscTrackCount,
                    Tracks = d.Select(t => new EditableAlbumTrack
                    {
                        TrackId = t.TrackId,
                        IdFromSource = t.IdFromSource,
                        ArtistId = t.ArtistId,
                        ArtistName = t.ArtistName,
                        TrackNo = t.TrackNo,
                        DiscNo = t.DiscNo,
                        Title = t.Title,
                        DurationSeconds = t.DurationSeconds
                    }).ToList()
                })
                .ToList()
        };
    }

    public EditableArtist MapEditableArtist(ArtistDetailsVM artist)
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

    public List<EditableArtistRelease> MapEditableArtistReleases(IReadOnlyCollection<AlbumVM> releases)
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

    public EditableTrack MapEditableTrack(TrackDetailsVM track)
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

    public AlbumDetailsVM MapEditedAlbum(EditableAlbum album)
    {
        return new AlbumDetailsVM
        {
            Id = album.Id,
            ArtistName = album.ArtistName,
            Title = album.Title,
            ReleaseType = album.ReleaseType,
            Year = album.Year,
            DiscCount = album.DiscCount,
            ArtworkBase64 = album.ArtworkBase64,
            Tags = album.Tags.ToList()
        };
    }

    public IReadOnlyCollection<AlbumTrackVM> MapEditedAlbumTracks(EditableAlbumDiscs discs)
    {
        var tracks = new List<AlbumTrackVM>();

        foreach (var disc in discs.Discs)
        {
            tracks.AddRange(disc.Tracks.Select(t => new AlbumTrackVM
            {
                TrackId = t.TrackId,
                TrackNo = t.TrackNo,
                DiscNo = t.DiscNo,
                DiscTrackCount = disc.TrackCount,
                Title = t.Title,
                ArtistId = t.ArtistId,
                ArtistName = t.ArtistName,
                IdFromSource = t.IdFromSource,
                DurationSeconds = t.DurationSeconds
            }));
        };

        return tracks;
    }

    public ArtistDetailsVM MapEditedArtist(EditableArtist artist)
    {
        return new ArtistDetailsVM
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

    public IReadOnlyCollection<AlbumVM> MapEditedArtistReleases(List<EditableArtistRelease> releases)
    {
        return releases
            .Select(a => new AlbumVM
            {
                Id = a.Id,
                Title = a.Title,
                ReleaseType = a.ReleaseType,
                Year = a.Year
            })
            .ToList();
    }

    public TrackDetailsVM MapEditedTrack(EditableTrack track)
    {
        return new TrackDetailsVM
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
