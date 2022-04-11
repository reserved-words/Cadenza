namespace Cadenza.Components.Tabs.Spotify;

public class SpotifySearchBase : ComponentBase
{
    [Inject]
    public ISpotifyLibrary Library { get; set; }

    public bool Searching { get; set; }

    public string SearchText { get; set; }

    public List<SpotifyArtist> ArtistSearchResults { get; set; }

    public async Task OnSearch()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            ArtistSearchResults = null;
            return;
        }

        Searching = true;
        ArtistSearchResults = await Library.Search(SearchText);
        Searching = false;
    }

    public async Task OnViewArtist(string id)
    {

    }
}
