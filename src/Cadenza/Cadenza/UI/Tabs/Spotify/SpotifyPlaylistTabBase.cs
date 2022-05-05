using Cadenza.Source.Spotify;
using Cadenza.Source.Spotify.Model;

namespace Cadenza.UI.Tabs.Spotify
{
    public class SpotifyPlaylistTabBase : ComponentBase
    {
        [Inject]
        public ISpotifyLibrary Library { get; set; }

        [Parameter]
        public SpotifyPlaylistSearchResult Model { get; set; }

        protected async Task AddAlbum(string id)
        {
            await Library.AddPlaylist(id);
        }
    }
}
