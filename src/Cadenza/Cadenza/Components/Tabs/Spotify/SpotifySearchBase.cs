using Cadenza.Components.Shared.Dialogs;

namespace Cadenza.Components.Tabs.Spotify;

public class SpotifySearchBase : ComponentBase
{
    [Inject]
    public ISpotifyLibrary Library { get; set; }

    [Inject]
    public IDialogService DialogService { get; set; }

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

    public async Task OnViewArtist(SpotifyArtist artist)
    {
        var albums = await Library.GetArtistAlbums(artist.Id);
        var artistProfile = new SpotifyArtistProfile
        {
            Artist = artist,
            Albums = albums
        };

        await DialogService.Display<SpotifyArtistView, SpotifyArtistProfile>(artistProfile, $"Spotify Artist - {artist.Name}");
    }
}
