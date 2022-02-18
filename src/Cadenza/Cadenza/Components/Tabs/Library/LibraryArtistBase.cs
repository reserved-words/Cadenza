using Cadenza.Common;
using Cadenza.Core.Model;
using Cadenza.Library;

namespace Cadenza;

public class LibraryArtistBase : ComponentBase
{
    [Inject]
    public IMergedArtistRepository Repository { get; set; }

    [Parameter]
    public string ArtistId { get; set; }

    public ArtistInfo Artist { get; set; }

    public List<ArtistReleaseGroup> Releases { get; set; } = new();

    public string PlaceholderText { get; set; }

    protected override void OnInitialized()
    {
        PlaceholderText = "No artist selected";
    }

    protected override async Task OnParametersSetAsync()
    {
		if (ArtistId == Artist?.Id)
			return;

		PlaceholderText = "Loading artist...";

		Artist = null;

		if (ArtistId != null)
		{
			await UpdateArtist();
		}

		PlaceholderText = "No artist selected";
	}

    private async Task UpdateArtist()
    {
        Artist = await Repository.GetArtist(ArtistId);

        var albums = await Repository.GetArtistAlbums(ArtistId);

        Releases = albums.GroupByReleaseType();

        StateHasChanged();
    }
}
