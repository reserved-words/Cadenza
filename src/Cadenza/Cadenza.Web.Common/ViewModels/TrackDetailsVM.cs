namespace Cadenza.Web.Common.ViewModels;

public class TrackDetailsVM : TrackVM
{
    //[ItemProperty(ItemProperty.TrackYear)]
    public string Year { get; set; }

    //[ItemProperty(ItemProperty.Lyrics)]
    public string Lyrics { get; set; }

    //[ItemProperty(ItemProperty.TrackTags)]
    public TagListVM Tags { get; set; } = new TagListVM();
}
