namespace Cadenza.Web.Common.ViewModels;

public class AlbumDetailsVM : AlbumVM
{
    public int DiscCount { get; set; }

    public List<int> TrackCounts { get; set; } = new List<int>();

    //[ItemProperty(ItemProperty.AlbumTags)]
    public TagListVM Tags { get; set; } = new TagListVM();
}
