namespace Cadenza.Web.Common.Model;

public class EditableAlbumDisc
{
    public List<EditableAlbumTrack> Tracks { get; set; }

    public int DiscNo { get; set; }
    public int TrackCount { get; set; }
}