namespace Cadenza.Web.Common.ViewModels;

public class ArtistDetailsVM : ArtistVM
{
    //[ItemProperty(ItemProperty.City)]
    public string City { get; set; }

    //[ItemProperty(ItemProperty.State)]
    public string State { get; set; }

    //[ItemProperty(ItemProperty.Country)]
    public string Country { get; set; }

    //[ItemProperty(ItemProperty.ArtistImage)]
    public string ImageBase64 { get; set; }

    //[ItemProperty(ItemProperty.ArtistTags)]
    public TagListVM Tags { get; set; } = new TagListVM();
}
