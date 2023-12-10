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

    public bool HaveAlbumTracksChanged(IReadOnlyCollection<AlbumTrackVM> originalTracks, IReadOnlyCollection<AlbumTrackVM> tracksAfterEdit, out IReadOnlyCollection<AlbumTrackVM> changedTracks)
    {
        var tracksWithChanges = new List<AlbumTrackVM>();

        foreach (var originalTrack in originalTracks)
        {
            var updatedTrack = tracksAfterEdit.SingleOrDefault(t => t.TrackId == originalTrack.TrackId);

            if (updatedTrack == null)
                continue; // removed

            if (HasAlbumTrackChanged(originalTrack, updatedTrack)) 
            {
                tracksWithChanges.Add(updatedTrack);
            }
        }

        changedTracks = tracksWithChanges;

        return changedTracks.Any();
    }

    private bool HasAlbumTrackChanged(AlbumTrackVM originalTrack, AlbumTrackVM updatedTrack)
    {
        return updatedTrack.Title != originalTrack.Title
            || updatedTrack.TrackNo != originalTrack.TrackNo
            || updatedTrack.DiscNo != originalTrack.DiscNo
            || updatedTrack.DiscTrackCount != originalTrack.DiscTrackCount;
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
