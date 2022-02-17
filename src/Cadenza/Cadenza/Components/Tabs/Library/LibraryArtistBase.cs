using Cadenza.Library;

namespace Cadenza;

public class LibraryArtistBase : ComponentBase
{
    [Inject]
    public IMergedArtistRepository Repository { get; set; }

    [Parameter]
    public string ArtistId { get; set; }

    public ArtistInfo Model { get; set; }

    public List<LibraryReleaseTypeGroup> Releases { get; set; } = new();

    public string PlaceholderText { get; set; }

    protected override void OnInitialized()
    {
        PlaceholderText = "No artist selected";
    }

    protected override async Task OnParametersSetAsync()
    {
		if (ArtistId == Model?.Id)
			return;

		PlaceholderText = "Loading artist...";

		Model = null;

		if (ArtistId != null)
		{
			await UpdateArtist();
		}

		PlaceholderText = "No artist selected";
	}

    private async Task UpdateArtist()
    {
        Model = await Repository.GetArtist(ArtistId);

        var albums = await Repository.GetArtistAlbums(ArtistId);

        Releases = albums
            .GroupBy(a => a.ReleaseType.GetAttribute<ReleaseTypeGroupAttribute>().Group)
            .Select(r => new LibraryReleaseTypeGroup
            {
                Group = r.Key,
                Albums = r.OrderBy(a => a.ReleaseType)
                    .ThenBy(a => a.Year)
                    .ToList()
            })
            .ToList();

        Releases = new List<LibraryReleaseTypeGroup>();

        StateHasChanged();
    }
}

public class LibraryReleaseTypeGroup
{
	public ReleaseTypeGroup Group { get; set; }
	public List<AlbumInfo> Albums { get; set; }
}