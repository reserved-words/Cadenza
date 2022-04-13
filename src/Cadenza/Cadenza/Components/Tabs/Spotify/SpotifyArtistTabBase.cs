namespace Cadenza.Components.Tabs.Spotify
{
    public class SpotifyArtistTabBase : ComponentBase
    {
        [Inject]
        public ISpotifyLibrary Library { get; set; }

        [Parameter]
        public SpotifyArtistSearchResult Model { get; set; }

        protected async Task AddAlbum(string id)
        {
            await Library.AddAlbum(id);
        }
        protected async Task AddPlaylist(string id)
        {
            await Library.AddPlaylist(id);
        }
    }
}
