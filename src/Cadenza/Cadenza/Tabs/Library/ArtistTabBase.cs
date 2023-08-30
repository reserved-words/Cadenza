namespace Cadenza.Tabs.Library;

public class ArtistTabBase : ComponentBase
{
    [Inject]
    public IArtistRepository Repository { get; set; }

    [Parameter]
    public int Id { get; set; }

    public ArtistInfo Artist { get; set; }

    public List<ArtistReleaseGroup> Releases { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        await UpdateArtist();
    }

    private async Task UpdateArtist()
    {
        Artist = await Repository.GetArtist(Id);

        var albumsByArtist = await Repository.GetAlbums(Id);

        var albumsFeaturingArtist = await Repository.GetAlbumsFeaturingArtist(Id);

        Releases = albumsByArtist
            .GroupByReleaseType()
            .AddAlbumsFeaturingArtist(albumsFeaturingArtist);

        StateHasChanged();
    }
}
