namespace Cadenza.Web.Common.ViewModels;

public class ArtistVM
{
    public int Id { get; set; }
    public string Name { get; set; }

    //[ItemProperty(ItemProperty.Grouping)]
    public GroupingVM Grouping { get; set; }

    //[ItemProperty(ItemProperty.Genre)]
    public string Genre { get; set; }

    public override string ToString() => Name;
}
