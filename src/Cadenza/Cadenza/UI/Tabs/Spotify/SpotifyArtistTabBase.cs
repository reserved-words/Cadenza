using Cadenza.Source.Spotify;
using Cadenza.Source.Spotify.Model;

namespace Cadenza.UI.Tabs.Spotify
{
    public class SpotifyArtistTabBase : ComponentBase
    {
        [Inject]
        public ISpotifyLibrary Library { get; set; }

        [Parameter]
        public SpotifyArtistSearchResult Model { get; set; }

        [Parameter]
        public Func<SpotifyAlbum, Task> OnShowAlbum { get; set; }

        [Parameter]
        public Func<SpotifyPlaylist, Task> OnShowPlaylist { get; set; }

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
