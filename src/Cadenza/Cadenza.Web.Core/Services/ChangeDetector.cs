using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Core.Services;

internal class ChangeDetector : IChangeDetector
{
    public bool HasAlbumChanged(AlbumDetailsVM originalAlbum, AlbumDetailsVM updatedAlbum)
    {
        return updatedAlbum.Title != originalAlbum.Title
            || updatedAlbum.ReleaseType != originalAlbum.ReleaseType
            || updatedAlbum.Year != originalAlbum.Year
            || updatedAlbum.DiscCount != originalAlbum.DiscCount
            || updatedAlbum.ArtworkBase64 != originalAlbum.ArtworkBase64
            || HaveTagsChanged(originalAlbum.Tags, updatedAlbum.Tags);
    }

    public bool HasArtistChanged(ArtistDetailsVM originalArtist, ArtistDetailsVM updatedArtist)
    {
        return updatedArtist.Name != originalArtist.Name
            || updatedArtist.Grouping.Name != originalArtist.Grouping.Name
            || updatedArtist.Genre != originalArtist.Genre
            || updatedArtist.City != originalArtist.City
            || updatedArtist.State != originalArtist.State
            || updatedArtist.Country != originalArtist.Country
            || updatedArtist.ImageBase64 != originalArtist.ImageBase64
            || HaveTagsChanged(originalArtist.Tags, updatedArtist.Tags); 
    }

    public bool HasTrackChanged(TrackDetailsVM originalTrack, TrackDetailsVM updatedTrack)
    {
        return updatedTrack.Title != originalTrack.Title
             || updatedTrack.Year != originalTrack.Year
             || updatedTrack.Lyrics != originalTrack.Lyrics
             || HaveTagsChanged(updatedTrack.Tags, originalTrack.Tags);
    }

    public bool HaveAlbumTracksChanged(IReadOnlyCollection<AlbumTrackVM> originalTracks, IReadOnlyCollection<AlbumTrackVM> tracksAfterEdit, out List<AlbumTrackVM> changedTracks)
    {
        changedTracks = [];

        foreach (var originalTrack in originalTracks)
        {
            var editedTrack = tracksAfterEdit.SingleOrDefault(t => t.TrackId == originalTrack.TrackId);

            if (editedTrack == null)
                continue;

            if (HasAlbumTrackChanged(originalTrack, editedTrack)) 
            {
                changedTracks.Add(editedTrack);
            }
        }

        return changedTracks.Count != 0;
    }

    public bool HaveArtistReleasesChanged(IReadOnlyCollection<AlbumVM> originalReleases, IReadOnlyCollection<AlbumVM> releasesAfterEdit, out List<AlbumVM> changedReleases)
    {
        changedReleases = [];

        foreach (var originalRelease in originalReleases)
        {
            var editedRelease = releasesAfterEdit.SingleOrDefault(r => r.Id == originalRelease.Id);

            if (editedRelease == null)
                continue;

            if (HasArtistReleaseChanged(originalRelease, editedRelease))
            {
                changedReleases.Add(editedRelease);
            }
        }

        return changedReleases.Count != 0;
    }

    private bool HasArtistReleaseChanged(AlbumVM originalRelease, AlbumVM editedRelease)
    {
        return editedRelease.Title != originalRelease.Title
            || editedRelease.ReleaseType != originalRelease.ReleaseType
            || editedRelease.Year != originalRelease.Year;
    }

    private bool HasAlbumTrackChanged(AlbumTrackVM originalTrack, AlbumTrackVM editedTrack)
    {
        return editedTrack.Title != originalTrack.Title
            || editedTrack.TrackNo != originalTrack.TrackNo
            || editedTrack.DiscNo != originalTrack.DiscNo
            || editedTrack.DiscTrackCount != originalTrack.DiscTrackCount;
    }

    private bool HaveTagsChanged(IReadOnlyCollection<string> originalTags, IReadOnlyCollection<string> newTags)
    {
        if (originalTags.Count != newTags.Count)
            return true;

        if (originalTags.Except(newTags).Any())
            return true;

        return false;
    }
}
