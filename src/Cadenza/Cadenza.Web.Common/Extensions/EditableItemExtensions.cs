namespace Cadenza.Web.Common.Extensions;

public static class EditableItemExtensions
{
    public static void AddTrack(this EditableAlbumDisc disc, EditableAlbumTrack track)
    {
        disc.Tracks.Add(track);
    }

    public static EditableAlbumDisc GetDisc(this List<EditableAlbumDisc> discs, EditableAlbumTrack track)
    {
        return discs.Single(d => d.Tracks.Contains(track));
    }

    public static EditableAlbumDisc GetDisc(this List<EditableAlbumDisc> discs, int discNo)
    {
        var disc = discs.SingleOrDefault(d => d.DiscNo == discNo);
        if (disc == null)
        {
            disc = new EditableAlbumDisc
            {
                DiscNo = discNo,
                TrackCount = 1,
                Tracks = []
            };
            discs.Add(disc);
        }

        return disc;
    }

    public static List<EditableAlbumDisc> SortAll(this List<EditableAlbumDisc> discs)
    {
        foreach (var disc in discs)
        {
            disc.SortTracks();
        }

        return discs.OrderBy(d => d.DiscNo).ToList();
    }

    public static void SortTracks(this EditableAlbumDisc disc)
    {
        disc.Tracks = disc.Tracks.OrderBy(t => t.TrackNo).ToList();
    }

    public static void UpdateTrackCount(this EditableAlbumDisc disc)
    {
        disc.TrackCount = Math.Max(disc.TrackCount, disc.Tracks.Count);
    }

    public static int GetDiscCount(this List<EditableAlbumDisc> discs)
    {
        return Math.Max(discs.Count, discs.Max(d => d.DiscNo));
    }
}
