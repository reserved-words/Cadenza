namespace Cadenza.Web.Common.Extensions;

public static class EditableItemExtensions
{
    public static void AddTrack(this EditableAlbumDisc disc, EditableAlbumTrack track)
    {
        disc.Tracks.Add(track);
    }

    public static EditableAlbumDisc GetDisc(this EditableAlbumDiscs discs, EditableAlbumTrack track)
    {
        return discs.Discs.Single(d => d.Tracks.Contains(track));
    }

    public static EditableAlbumDisc GetDisc(this EditableAlbumDiscs discs, int discNo)
    {
        var disc = discs.Discs.SingleOrDefault(d => d.DiscNo == discNo);
        if (disc == null)
        {
            disc = new EditableAlbumDisc
            {
                DiscNo = discNo,
                TrackCount = 1,
                Tracks = []
            };
            discs.Discs.Add(disc);
        }

        return disc;
    }

    public static List<EditableAlbumDisc> SortAll(this EditableAlbumDiscs discs)
    {
        foreach (var disc in discs.Discs)
        {
            disc.SortTracks();
        }

        return discs.Discs.OrderBy(d => d.DiscNo).ToList();
    }

    public static void SortTracks(this EditableAlbumDisc disc)
    {
        disc.Tracks = disc.Tracks.OrderBy(t => t.TrackNo).ToList();
    }

    public static void UpdateTrackCount(this EditableAlbumDisc disc)
    {
        disc.TrackCount = Math.Max(disc.TrackCount, disc.Tracks.Count);
    }

    public static int GetDiscCount(this EditableAlbumDiscs discs)
    {
        return Math.Max(discs.Discs.Count, discs.Discs.Max(d => d.DiscNo));
    }
}
