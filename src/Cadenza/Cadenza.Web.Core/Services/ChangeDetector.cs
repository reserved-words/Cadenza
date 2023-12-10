using Cadenza.Web.Common.ViewModel;

namespace Cadenza.Web.Core.Services;

internal class ChangeDetector : IChangeDetector
{
    public bool HasTrackChanged(TrackDetailsVM originalTrack, TrackDetailsVM editedTrack)
    {
        return editedTrack.Title != originalTrack.Title
             || editedTrack.Year != originalTrack.Year
             || editedTrack.Lyrics != originalTrack.Lyrics
             || HaveTagsChanged(editedTrack.Tags, originalTrack.Tags);
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
