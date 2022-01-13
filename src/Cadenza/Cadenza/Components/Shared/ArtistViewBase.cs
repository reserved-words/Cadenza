using Cadenza.Database;

namespace Cadenza;

public class ArtistViewBase : ComponentBase
{
    [Parameter]
    public LibraryArtistDetails Model { get; set; }

    [Parameter]
    public List<LibrarySource> Sources { get; set; }

    [Parameter]
    public bool HeaderOnly { get; set; }

    private static List<LinkType> LinkTypes => Enum.GetValues<LinkType>().ToList();

    public List<LinkViewModel> ArtistLinks { get; set; } = new List<LinkViewModel>();

    protected override void OnParametersSet()
    {
        ArtistLinks.Clear();
        ArtistLinks = LinkTypes.Select(GetLinkViewModel).ToList();
    }

    private LinkViewModel GetLinkViewModel(LinkType linkType)
    {
        var link = Model.Links?.FirstOrDefault(l => l.Type == linkType);
        var name = link?.Name ?? linkType.GetDefault(Model.Name);
        var url = linkType.GetUrl(name);

        return new LinkViewModel
        {
            Type = linkType,
            Url = url,
            Disabled = link != null && link.Name == null
        };
    }

    protected string Location => AsList(Model.City, Model.State, Model.Country);

    private string AsList(params string[] city)
    {
        return string.Join(", ", city.Where(s => !string.IsNullOrWhiteSpace(s)));
    }
}